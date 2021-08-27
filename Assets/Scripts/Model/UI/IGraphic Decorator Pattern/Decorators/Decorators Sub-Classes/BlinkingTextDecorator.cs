using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BlinkingTextDecorator : ButtonDecorator
{
    /* Add blinking animation
     * 
     */

    private MonoBehaviour mono;
    private TextMeshProUGUI text;

    public BlinkingTextDecorator(IGraphicComponent component, MonoBehaviour mono, TextMeshProUGUI text) : base(component)
    {
        if (!mono)
        {
            LogWarning("The MonoBehaviour is null");
            return;
        }
        else if (!text)
        {
            LogWarning("The text argument is null");
            return;
        }

        this.mono = mono;
        this.text = text;
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        if (!buttonInstance.Equals(null))
        {
            mono.StopCoroutine(typeof(CustomDotTween).GetMethods().Where(x => x.Name.Equals("BlinkingTextUI")).FirstOrDefault().Name);
            mono.StartCoroutine(CustomDotTween.BlinkingTextUI(text, Globals.blinkingTime, Globals.blinkPerSecond));
        }
    }

    public override void OnPointerEnter(PointerEventData eventData) { }

    public override void OnPointerExit(PointerEventData eventData) { }

    #region private functions
    private void LogWarning(string msg) => Debug.LogWarning("[Blinking Text Decorator] : " + msg);
    #endregion
}
