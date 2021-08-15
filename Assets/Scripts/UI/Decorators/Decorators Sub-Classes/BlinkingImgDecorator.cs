using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class BlinkingImgDecorator : ImgDecorator
{
    #region public functions

    #region events
    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
        PlayGraphicAnimation();
    }
    #endregion

    public override void PlayGraphicAnimation()
    {
        base.PlayGraphicAnimation();
        BlinkingImg();
    }

    #endregion

    #region private functions

    private void BlinkingImg()
    {
        StopCoroutine(typeof(CustomDotTween).GetMethods().Where(x => x.Name == "BlinkingImgUI").FirstOrDefault().Name);
        StartCoroutine(CustomDotTween.BlinkingImgUI(imgInstance.img, Globals.blinkingTime, 5));
    }

    #endregion
}
