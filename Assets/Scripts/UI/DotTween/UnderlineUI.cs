using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnderlineUI : MonoBehaviour, IPointerEnterHandler, UnityEngine.EventSystems.IPointerExitHandler
{
    private TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        text.fontStyle |= FontStyles.Underline;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        text.fontStyle &= ~FontStyles.Underline;
    }
}
