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
    private float deltaY;

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
        this.deltaY = rect.sizeDelta.y / 2;
    }

    #region interface
    public override void OnPointerClick(PointerEventData eventData) { }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        if (!panelInstance.Equals(null))
        {
            mono.StopCoroutine(typeof(CustomDotTween).GetMethods().Where(x => x.Name.Equals("FocusAlt")).FirstOrDefault().Name);
            mono.StartCoroutine(CustomDotTween.FocusAlt(rect, anchorY - deltaY + Globals.playerselectoffset, Globals.buzzingTime));
        }
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        if (!panelInstance.Equals(null))
        {
            mono.StopCoroutine(typeof(CustomDotTween).GetMethods().Where(x => x.Name.Equals("FocusAlt")).FirstOrDefault().Name);
            mono.StartCoroutine(CustomDotTween.FocusAlt(rect, anchorY - deltaY, Globals.buzzingTime));
        }
    }
    #endregion

    #region private functions
    private void LogWarning(string msg) => Debug.LogWarning("[Focus panel Decorator] : " + msg);
    #endregion
}
