using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ColorToggleUI : MonoBehaviour, IPointerClickHandler
{
    private TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void OnPointerClick(PointerEventData eventData) => text.color = Color.white;
}
