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

    private Image alphaImg;

    public SelectableImgPanelDecorator(IGraphicComponent component, Image alphaImg) : base(component)
    {
        if (!alphaImg)
        {
            LogWarning("The image component is null");
            return;
        }

        this.alphaImg = alphaImg;
        this.alphaImg.color = CustomDotTween.UpdateColor(Color.grey);
    }

    public override void OnPointerClick(PointerEventData eventData) { }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        alphaImg.color = CustomDotTween.UpdateColor(Color.white);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        alphaImg.color = CustomDotTween.UpdateColor(Color.grey);
    }

    #region private functions
    private void LogWarning(string msg) => Debug.LogWarning("[Selectable img Decorator] : " + msg);
    #endregion
}
