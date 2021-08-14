using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnderlineDecorator : ButtonDecorator, IPointerExitHandler
{
    #region public functions

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        buttonInstance.text.fontStyle |= FontStyles.Underline;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonInstance.text.fontStyle &= ~FontStyles.Underline;
    }

    #endregion
}
