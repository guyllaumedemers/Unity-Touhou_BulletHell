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

        RegisterOperation(new BlinkingTextDecorator(this, mono, text));
        RegisterOperation(new UnderlineDecorator(this, text));
    }

    #region interface
    public override void OnPointerClick(PointerEventData eventData)
    {
        foreach (var item in buttonModifiers) item.OnPointerClick(eventData);
        if (button.interactable)
        {
            AudioManager.Instance.TriggerButtonClickSFX();
            UpdateTextComponentsColor(text, alternateText);
        }
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        foreach (var item in buttonModifiers) item.OnPointerEnter(eventData);
        if (button.interactable) AudioManager.Instance.TriggerMouseSFX();
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        foreach (var item in buttonModifiers) item.OnPointerExit(eventData);
    }
    #endregion

    #region private functions
    private void UpdateTextComponentsColor(TextMeshProUGUI src, TextMeshProUGUI target)
    {
        if (!src || !target)
        {
            return;
        }

        Color temp = src.color;
        src.color = target.color;
        target.color = temp;
    }
    private void LogWarning(string msg) => Debug.LogWarning("[Option Menu Button Component] : " + msg);
    #endregion
}
