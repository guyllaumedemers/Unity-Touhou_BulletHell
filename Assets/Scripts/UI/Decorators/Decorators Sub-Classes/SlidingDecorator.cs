using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlidingDecorator : ImgDecorator, IPointerExitHandler
{
    public Coroutine routine;
    public RectTransform rect { get; private set; }
    public float anchpos { get; private set; }

    private void Awake()
    {
        rect = GameObject.FindGameObjectWithTag(Globals.slidingComponent).GetComponent<RectTransform>();
        if (!rect)
        {
            LogWarning($"This gameobject is not a UI element {gameObject.name}");
            return;
        }
        anchpos = rect.anchoredPosition.x;
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        if (!rect)
        {
            LogWarning("There is no RectTransform attach to the script");
            return;
        }
        SlideAnimation(imgInstance.anchpos);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!rect)
        {
            LogWarning("There is no RectTransform attach to the script");
            return;
        }
        SlideAnimation(imgInstance.anchpos - Globals.sliding_offset);
    }

    #region private functions

    private void SlideAnimation(float endAnchorpos)
    {
        StopCoroutine(typeof(CustomDotTween).GetMethods().Where(x => x.Name == "SlideAnimation").FirstOrDefault().Name);
        StartCoroutine(CustomDotTween.SlideAnimation(rect, endAnchorpos, Globals.slidingTime));
    }

    private void LogWarning(string msg) => Debug.LogWarning("[Sliding UI] " + msg);

    #endregion
}
