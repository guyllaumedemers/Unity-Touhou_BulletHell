using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OnButtonEventHandler : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        ResetColor(button.colors.disabledColor, Color.white);
    }

    public void OnPointerClick(PointerEventData eventData) => AudioManager.Instance.TriggerButtonClickSFX();

    public void OnPointerEnter(PointerEventData eventData) => AudioManager.Instance.TriggerMouseSFX();

    private void ResetColor(Color old, Color newc)
    {
        old.r = newc.r;
        old.g = newc.g;
        old.b = newc.b;
        old.a = newc.a;
    }
}
