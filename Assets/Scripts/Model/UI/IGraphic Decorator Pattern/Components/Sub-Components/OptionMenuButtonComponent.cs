using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OptionMenuButtonComponent : ButtonComponent
{
    public TextMeshProUGUI alternateText;

    protected override void Awake()
    {
        this.mono = this;
        this.text = GetComponentInChildren<TextMeshProUGUI>();
        this.button = GetComponent<Button>();

        if (!mono)
        {
            LogWarning("Wait what how can it be attach to a gameobject if not monobehaviour, did you break Unity?");
            return;
        }
        else if (!text)
        {
            LogWarning("There is no text component attach to this gameobject " + gameObject.name);
            return;
        }
        else if (!button)
        {
            LogWarning("There is no button component attach to this gameobject " + gameObject.name);
            return;
        }
        else if (!alternateText)
        {
            LogWarning("You have not assign the opposite text component to the script \n if the number of log warning exceed 3 logs, an error as occured " + gameObject.name);
        }

        if (mono.gameObject.tag != Globals.off) text.color = CustomDotTween.UpdateColor(Color.white);
        else text.color = CustomDotTween.UpdateColor(Color.grey);

        RegisterOperation(new UnderlineDecorator(this, text));
    }

    #region interface

    public override void OnPointerEnter(PointerEventData eventData)
    {
        if (button.interactable)
        {
            AudioManager.Instance.TriggerMouseSFX();
            foreach (var item in buttonModifiers) item.OnPointerEnter(eventData);
        }
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        if (button.interactable)
        {
            foreach (var item in buttonModifiers) item.OnPointerExit(eventData);
        }
    }
    #endregion

    #region private functions
    private void LogWarning(string msg) => Debug.LogWarning("[Option Menu Button Component] : " + msg);
    #endregion
}
