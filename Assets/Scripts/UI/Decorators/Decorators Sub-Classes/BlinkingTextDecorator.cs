using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class BlinkingTextDecorator : ButtonDecorator
{
    MonoBehaviour mono;
    TextMeshProUGUI text;

    public BlinkingTextDecorator(IGraphicComponent component, MonoBehaviour mono, TextMeshProUGUI text) : base(component)
    {
        this.mono = mono;
        this.text = text;

        if (!mono)
        {
            LogWarning("Wait what how can it be attach to a gameobject if not monobehaviour, did you break Unity?");
            return;
        }
        else if (!text)
        {
            LogWarning("There is no text component attach to this gameobject " + mono.gameObject.name);
            return;
        }
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
