using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuzzingDecorator : ButtonDecorator
{
    /*  Add buzzing animation
     * 
     */

    MonoBehaviour mono;
    RectTransform rect;

    public BuzzingDecorator(IGraphicComponent component, MonoBehaviour mono, RectTransform rect) : base(component)
    {
        if (!mono)
        {
            LogWarning("The MonoBehaviour is null");
            return;
        }
        else if (!rect)
        {
            LogWarning("The rect argument is null");
            return;
        }

        this.mono = mono;
        this.rect = rect;
    }

    public override void OnPointerClick(PointerEventData eventData) { }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        mono.StopCoroutine(typeof(CustomDotTween).GetMethods().Where(x => x.Name.Equals("BuzzingUI")).FirstOrDefault().Name);
        mono.StartCoroutine(CustomDotTween.BuzzingUI(rect, Globals.buzzingTime));
    }

    public override void OnPointerExit(PointerEventData eventData) { }

    #region private functions
    private void LogWarning(string msg) => Debug.LogWarning("[Buzzing Decorator] : " + msg);
    #endregion
}
