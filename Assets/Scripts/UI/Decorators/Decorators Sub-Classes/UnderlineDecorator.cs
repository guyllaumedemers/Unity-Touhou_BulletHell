using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnderlineDecorator : ButtonDecorator, IPointerExitHandler
{
    #region public functions

    #region events
    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        PlayGraphicAnimation();
    }

    public void OnPointerExit(PointerEventData eventData) => PlayGraphicAnimation();
    #endregion

    public override void PlayGraphicAnimation()
    {
        base.PlayGraphicAnimation();
        AddRemoveFontStyle();
    }

    #endregion

    #region private functions

    private void AddRemoveFontStyle()
    {
        if (!buttonInstance.text.fontStyle.HasFlag(FontStyles.Underline)) buttonInstance.text.fontStyle |= FontStyles.Underline;
        else buttonInstance.text.fontStyle &= ~FontStyles.Underline;
    }

    #endregion
}
