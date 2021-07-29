using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class BlinkingUI : MonoBehaviour, IPointerClickHandler
{
    private TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        StopCoroutine("BlinkingUI");
        StartCoroutine(CustomDotTween.BlinkingUI(text, Globals.blinkingTime, 5));
    }
}
