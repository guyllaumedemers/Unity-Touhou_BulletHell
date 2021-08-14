using UnityEngine;
using UnityEngine.EventSystems;

public class SlidingDecorator : ImgDecorator, IPointerExitHandler
{
    public Coroutine routine;

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        if (!imgInstance.descriptionRect)
        {
            LogWarning("There is no RectTransform attach to the script");
            return;
        }

        this.EnsureRoutineStop(ref routine);
        this.CreateAnimationRoutine(Globals.slidingTime, delegate (float progress)
        {
            float ease = EasingFunction.EaseInOutSine(0, 1, progress);
            Vector2 start = imgInstance.descriptionRect.anchoredPosition;
            Vector2 end = new Vector2(imgInstance.anchpos, imgInstance.descriptionRect.anchoredPosition.y);
            imgInstance.descriptionRect.anchoredPosition = Vector2.Lerp(start, end, ease);
        },
        delegate
        {
            routine = null;
        });
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!imgInstance.descriptionRect)
        {
            LogWarning("There is no RectTransform attach to the script");
            return;
        }

        this.EnsureRoutineStop(ref routine);
        this.CreateAnimationRoutine(Globals.slidingTime, delegate (float progress)
        {
            float ease = EasingFunction.EaseInOutSine(0, 1, progress);
            Vector2 start = imgInstance.descriptionRect.anchoredPosition;
            Vector2 end = new Vector2(imgInstance.anchpos - Globals.sliding_offset, imgInstance.descriptionRect.anchoredPosition.y);
            imgInstance.descriptionRect.anchoredPosition = Vector2.Lerp(start, end, ease);
        },
        delegate
        {
            routine = null;
        });
    }

    private void LogWarning(string msg) => Debug.LogWarning("[Sliding UI] " + msg);
}
