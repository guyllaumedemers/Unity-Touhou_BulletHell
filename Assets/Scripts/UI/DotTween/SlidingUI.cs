using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements.Experimental;

public class SlidingUI : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public RectTransform descriptionRect;
    private Coroutine routine;

    public void OnPointerClick(PointerEventData eventData)
    {
        //TODO Selection Confirm

        //UIManager.Instance.LaunchGameScene();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!descriptionRect)
        {
            LogWarning("There is no RectTransform attach to the script");
            return;
        }

        this.EnsureRoutineStop(ref routine);
        routine = this.CreateAnimationRoutine(Globals.slidingTime, delegate (float progress)
        {
            float eased = EasingFunction.EaseInOutSine(0, 1, progress);
            descriptionRect.anchoredPosition = Vector2.Lerp(new Vector2(descriptionRect.anchoredPosition.x, descriptionRect.anchoredPosition.y),
                new Vector2(descriptionRect.anchoredPosition.x - Globals.sliding_offset, descriptionRect.anchoredPosition.y), eased);
        },
        delegate { routine = null; });
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!descriptionRect)
        {
            LogWarning("There is no RectTransform attach to the script");
            return;
        }

        this.EnsureRoutineStop(ref routine);
        routine = this.CreateAnimationRoutine(Globals.slidingTime, delegate (float progress)
        {
            float eased = EasingFunction.EaseInOutSine(0, 1, progress);
            descriptionRect.anchoredPosition = Vector2.Lerp(new Vector2(descriptionRect.anchoredPosition.x, descriptionRect.anchoredPosition.y),
                new Vector2(descriptionRect.anchoredPosition.x + Globals.sliding_offset, descriptionRect.anchoredPosition.y), eased);
        },
        delegate { routine = null; });
    }

    private void LogWarning(string msg) => Debug.LogWarning("[Sliding UI] " + msg);
}
