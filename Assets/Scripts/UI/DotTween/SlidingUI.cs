using UnityEngine;
using UnityEngine.EventSystems;

public class SlidingUI : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, UnityEngine.EventSystems.IPointerExitHandler
{
    public RectTransform descriptionRect;

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
        StartCoroutine(CustomDotTween.SlidingUI(descriptionRect, descriptionRect.anchorMax.x, descriptionRect.anchorMin.x, 0.1f));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!descriptionRect)
        {
            LogWarning("There is no RectTransform attach to the script");
            return;
        }
        StopCoroutine("SlidingUI");
        StartCoroutine(CustomDotTween.SlidingUI(descriptionRect, descriptionRect.anchorMin.x, descriptionRect.anchorMax.x, 0.1f));
    }

    private void LogWarning(string msg) => Debug.LogWarning("[Sliding UI] " + msg);
}
