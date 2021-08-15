using System.Linq;
using UnityEngine.EventSystems;

public class BuzzingDecorator : ButtonDecorator
{
    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        BuzzAnimation();
    }

    #region private functions

    private void BuzzAnimation()
    {
        StopCoroutine(typeof(CustomDotTween).GetMethods().Where(x => x.Name == "BuzzingUI").FirstOrDefault().Name);
        StartCoroutine(CustomDotTween.BuzzingUI(buttonInstance.rect, Globals.buzzingTime));
    }

    #endregion
}
