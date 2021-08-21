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
    float deltaY;

    public FocusPanelDecorator(IGraphicComponent component, MonoBehaviour mono, RectTransform rect) : base(component)
    {
        this.mono = mono;
        this.rect = rect;
        if (!mono)
        {
            LogWarning("Wait what how can it be attach to a gameobject if not monobehaviour, did you break Unity?");
            return;
        }
        else if (!rect)
        {
            LogWarning("Rect is null " + mono.gameObject.name);
            return;
        }
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
