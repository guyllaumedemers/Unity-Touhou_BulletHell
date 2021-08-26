using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnderlineDecorator : ButtonDecorator
{
    /*  Add underline
     * 
     */

    TextMeshProUGUI text;

    public UnderlineDecorator(IGraphicComponent component, TextMeshProUGUI text) : base(component)
    {
        if (!text)
        {
            LogWarning("The text argument is null");
            return;
        }

        this.text = text;
    }

    public override void OnPointerClick(PointerEventData eventData) { }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        text.fontStyle |= FontStyles.Underline;
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        text.fontStyle &= ~FontStyles.Underline;
    }

    #region private functions
    private void LogWarning(string msg) => Debug.LogWarning("[Underline Decorator] : " + msg);
    #endregion
}
