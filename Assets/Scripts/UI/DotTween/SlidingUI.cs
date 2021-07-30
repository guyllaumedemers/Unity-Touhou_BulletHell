using UnityEngine;
using UnityEngine.EventSystems;

public class SlidingUI : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public RectTransform descriptionRect;
    private float xMin;
    private float xMax;

    private void Awake()
    {
        if (!descriptionRect)
        {
            LogWarning("There is no RectTransform attach to the script");
            return;
        }
        xMin = descriptionRect.anchorMin.x;
        xMax = descriptionRect.anchorMax.x;
    }

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
        StopCoroutine("SlidingUI");
        StartCoroutine(CustomDotTween.SlidingUI(descriptionRect, xMax, xMin, 0.1f));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!descriptionRect)
        {
            LogWarning("There is no RectTransform attach to the script");
            return;
        }
        StopCoroutine("SlidingUI");
        StartCoroutine(CustomDotTween.SlidingUI(descriptionRect, xMin, xMax, 0.1f));
    }

    private void LogWarning(string msg) => Debug.LogWarning("[Sliding UI] " + msg);
}
