using UnityEngine;
using UnityEngine.EventSystems;

public class WidgetUI : MonoBehaviour, IPointerEnterHandler
{
    private RectTransform rect;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        StopCoroutine("WidgetUI");
        StartCoroutine(CustomDotTween.WidgetUI(rect, Globals.widgetTime));
    }
}
