using UnityEngine;
using UnityEngine.EventSystems;

public class SlidingUI : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public RectTransform descriptionRect;
    private Coroutine routine;
    private float anchpos;

    private void Awake()
    {
        if (!descriptionRect)
        {
            LogWarning("There is no RectTransform attach to the script");
            return;
        }
        anchpos = descriptionRect.anchoredPosition.x;
    }

    public void OnPointerClick(PointerEventData eventData) => UIManager.Instance.LaunchGameScene();

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!descriptionRect)
        {
            LogWarning("There is no RectTransform attach to the script");
            return;
        }

        this.EnsureRoutineStop(ref routine);
        this.CreateAnimationRoutine(Globals.slidingTime, delegate (float progress)
        {
            float ease = EasingFunction.EaseInOutSine(0, 1, progress);
            Vector2 start = descriptionRect.anchoredPosition;
            Vector2 end = new Vector2(anchpos - Globals.sliding_offset, descriptionRect.anchoredPosition.y);
            descriptionRect.anchoredPosition = Vector2.Lerp(start, end, ease);
        },
        delegate
        {
            routine = null;
        });
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!descriptionRect)
        {
            LogWarning("There is no RectTransform attach to the script");
            return;
        }

        this.EnsureRoutineStop(ref routine);
        this.CreateAnimationRoutine(Globals.slidingTime, delegate (float progress)
        {
            float ease = EasingFunction.EaseInOutSine(0, 1, progress);
            Vector2 start = descriptionRect.anchoredPosition;
            Vector2 end = new Vector2(anchpos, descriptionRect.anchoredPosition.y);
            descriptionRect.anchoredPosition = Vector2.Lerp(start, end, ease);
        },
        delegate
        {
            routine = null;
        });
    }

    private void LogWarning(string msg) => Debug.LogWarning("[Sliding UI] " + msg);
}
