using System.Linq;
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

        xMin = Camera.main.WorldToScreenPoint(Utilities.WorldSpaceAnchors(descriptionRect)[0]).x;
        xMax = Camera.main.WorldToScreenPoint(Utilities.WorldSpaceAnchors(descriptionRect)[3]).x;
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

        StopCoroutine(typeof(CustomDotTween).GetMethods().Where(x => x.Name == "SlidingUI").FirstOrDefault().ToString());
        StartCoroutine(CustomDotTween.SlidingUI(descriptionRect, xMax, xMin, Globals.slidingTime));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!descriptionRect)
        {
            LogWarning("There is no RectTransform attach to the script");
            return;
        }

        StopCoroutine(typeof(CustomDotTween).GetMethods().Where(x => x.Name == "SlidingUI").FirstOrDefault().ToString());
        StartCoroutine(CustomDotTween.SlidingUI(descriptionRect, xMin, xMax, Globals.slidingTime));
    }

    private void LogWarning(string msg) => Debug.LogWarning("[Sliding UI] " + msg);
}
