using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlidingDecorator : ImgDecorator, IPointerExitHandler
{
    public Coroutine routine;

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        if (!imgInstance.rect)
        {
            LogWarning("There is no RectTransform attach to the script");
            return;
        }
        SlideAnimation(imgInstance.anchpos);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!imgInstance.rect)
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
        StartCoroutine(CustomDotTween.SlideAnimation(imgInstance.rect, endAnchorpos, Globals.slidingTime));
    }

    private void LogWarning(string msg) => Debug.LogWarning("[Sliding UI] " + msg);

    #endregion
}
