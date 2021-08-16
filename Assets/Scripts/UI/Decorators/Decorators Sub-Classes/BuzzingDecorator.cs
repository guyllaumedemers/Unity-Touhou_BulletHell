using System.Linq;
using UnityEngine.EventSystems;

public class BuzzingDecorator : ButtonDecorator
{
    #region public functions

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        PlayGraphicAnimation();
    }

    public override void PlayGraphicAnimation()
    {
        base.PlayGraphicAnimation();
        BuzzAnimation();
    }

    #endregion

    #region private functions

    private void BuzzAnimation()
    {
        StopCoroutine(typeof(CustomDotTween).GetMethods().Where(x => x.Name == "BuzzingUI").FirstOrDefault().Name);
        StartCoroutine(CustomDotTween.BuzzingUI(buttonInstance.rect, Globals.buzzingTime));
    }

    #endregion
}
