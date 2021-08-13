using System.Linq;
using UnityEngine.EventSystems;

public class BlinkingTextDecorator : ButtonDecorator
{
    public override void PlayGraphicAnimation()
    {
        base.PlayGraphicAnimation();
        Blinkingtext();
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
        PlayGraphicAnimation();
    }

    #region private function

    private void Blinkingtext()
    {
        StopCoroutine(typeof(CustomDotTween).GetMethods().Where(x => x.Name == "BlinkingTextUI").FirstOrDefault().Name);
        StartCoroutine(CustomDotTween.BlinkingTextUI(buttonInstance.text, Globals.blinkingTime, 5));
    }

    #endregion
}
