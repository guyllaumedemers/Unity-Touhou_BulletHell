using UnityEngine;
using UnityEngine.EventSystems;

public class OnButtonHoovering : MonoBehaviour, IPointerEnterHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        AudioManager.Instance.TriggerMouseSFX();
    }
}
