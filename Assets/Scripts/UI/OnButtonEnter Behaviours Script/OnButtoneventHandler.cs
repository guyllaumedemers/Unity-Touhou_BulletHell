using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OnButtonEventHandler : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData) => AudioManager.Instance.TriggerButtonClickSFX();

    public void OnPointerEnter(PointerEventData eventData) => AudioManager.Instance.TriggerMouseSFX();
}
