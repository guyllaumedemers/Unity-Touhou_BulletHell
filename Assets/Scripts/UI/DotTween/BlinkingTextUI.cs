using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class BlinkingTextUI : MonoBehaviour, IPointerClickHandler
{
    private TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        StopCoroutine(typeof(CustomDotTween).GetMethods().Where(x => x.Name == "BlinkingTextUI").FirstOrDefault().Name);
        StartCoroutine(CustomDotTween.BlinkingTextUI(text, Globals.blinkingTime, 5));
    }
}
