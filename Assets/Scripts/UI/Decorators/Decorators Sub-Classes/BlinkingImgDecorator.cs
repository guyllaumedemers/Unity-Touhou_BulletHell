using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BlinkingImgDecorator : PanelDecorator
{
    private MonoBehaviour mono;
    private Image playerImg;

    public BlinkingImgDecorator(IGraphicComponent component, MonoBehaviour mono, Image image) : base(component)
    {
        if (!mono)
        {
            LogWarning("MonoBehaviour is null");
            return;
        }
        else if (!image)
        {
            LogWarning("Img is null " + mono.gameObject.name);
            return;
        }

        this.mono = mono;
        this.playerImg = image;
    }


    public override void OnPointerClick(PointerEventData eventData)
    {
        mono.StopCoroutine(typeof(CustomDotTween).GetMethods().Where(x => x.Name.Equals("BlinkingImgUI")).FirstOrDefault().Name);
        mono.StartCoroutine(CustomDotTween.BlinkingImgUI(playerImg, Globals.blinkingTime, Globals.blinkPerSecond));
    }

    public override void OnPointerEnter(PointerEventData eventData) { }

    public override void OnPointerExit(PointerEventData eventData) { }

    #region private functions
    private void LogWarning(string msg) => Debug.LogWarning("[Blinking Img Decorator] : " + msg);
    #endregion
}
