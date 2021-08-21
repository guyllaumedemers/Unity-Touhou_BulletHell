using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectableImgPanelDecorator : PanelDecorator
{
    /*  OnPointerEnter, OnPointerExit and OnPointerClick behaviours
     *  
     *  highlight, reset, blink
     * 
     */
    public SelectableImgPanelDecorator(IGraphicComponent component) : base(component) { }

    public override void OnPointerClick(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }
}
