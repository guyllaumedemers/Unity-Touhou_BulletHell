using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class FocusPanelDecorator : PanelDecorator
{
    /*  Slide up the Tab
     * 
     */

    MonoBehaviour mono;
    RectTransform rect;
    float anchorY;

    public FocusPanelDecorator(IGraphicComponent component, MonoBehaviour mono, RectTransform rect) : base(component)
    {
        this.mono = mono;
        this.rect = rect;
        if (!rect)
        {
            LogWarning("Rect is null " + mono.gameObject.name);
            return;
        }
        this.anchorY = rect.anchoredPosition.y;
    }

    #region interface
    public override void OnPointerClick(PointerEventData eventData) { }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        if (!panelInstance.Equals(null))
        {
            mono.StopCoroutine(typeof(CustomDotTween).GetMethods().Where(x => x.Name.Equals("FocusAlt")).FirstOrDefault().Name);
            mono.StartCoroutine(CustomDotTween.FocusAlt(rect, anchorY - rect.sizeDelta.y / 2 + Globals.playerselectoffset, Globals.buzzingTime));
        }
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        if (!panelInstance.Equals(null))
        {
            mono.StopCoroutine(typeof(CustomDotTween).GetMethods().Where(x => x.Name.Equals("FocusAlt")).FirstOrDefault().Name);
            mono.StartCoroutine(CustomDotTween.FocusAlt(rect, anchorY - rect.sizeDelta.y / 2, Globals.buzzingTime));
        }
    }
    #endregion

    #region private functions
    private void LogWarning(string msg) => Debug.LogWarning("[Focus panel Decorator] : " + msg);
    #endregion
}
