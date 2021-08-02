using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BlinkingImgUI : MonoBehaviour, IPointerClickHandler
{
    private Image img;

    private void Awake()
    {
        img = GetComponentInChildren<Image>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        StopCoroutine(typeof(CustomDotTween).GetMethods().Where(x => x.Name == "BlinkingImgUI").FirstOrDefault().Name);
        StartCoroutine(CustomDotTween.BlinkingImgUI(img, Globals.blinkingTime, 5));
    }
}
