using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuzzingUI : MonoBehaviour, IPointerEnterHandler
{
    private RectTransform rect;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        StopCoroutine(typeof(CustomDotTween).GetMethods().Where(x => x.Name == "BuzzingUI").FirstOrDefault().Name);
        StartCoroutine(CustomDotTween.BuzzingUI(rect, Globals.widgetTime));
    }
}
