using UnityEngine;
using UnityEngine.EventSystems;

public class UniqueInteractionButtonComponent : ButtonComponent
{
    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
        if (button.interactable)
        {
            button.interactable = false;
        }
    }
}
