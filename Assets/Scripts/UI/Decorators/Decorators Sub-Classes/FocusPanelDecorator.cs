using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class FocusPanelDecorator : PanelDecorator
{
    /*  Slide up the Tab
     * 
     */

    private MonoBehaviour mono;
    private RectTransform rect;
    private float anchorY;

    public FocusPanelDecorator(IGraphicComponent component, MonoBehaviour mono, RectTransform rect) : base(component)
    {
        if (!mono)
        {
            LogWarning("The MonoBehaviour is null");
            return;
        }
        else if (!rect)
        {
            LogWarning("The rect argument is null");
            return;
        }

        this.mono = mono;
        this.rect = rect;
        this.anchorY = rect.anchoredPosition.y;
    }

    #region interface
    public override void OnPointerClick(PointerEventData eventData) { }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        /*  rect.sizedelta cannot be set in the constructor as it needs to wait one frame before being able to retrieve the value
         *  of the rectransform
         * 
         */
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
