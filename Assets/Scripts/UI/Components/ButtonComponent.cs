using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonComponent : MonoBehaviour, IGraphicComponent
{
    public List<ButtonDecorator> buttonModifiers = new List<ButtonDecorator>();
    protected MonoBehaviour mono;
    protected Button button;
    protected TextMeshProUGUI text;

    protected virtual void Awake()
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

        text.color = CustomDotTween.UpdateColor(Color.grey);
        RegisterOperation(new BlinkingTextDecorator(this, mono, text));
    }

    #region interface
    public void OnPointerClick(PointerEventData eventData)
    {
        foreach (var item in buttonModifiers) item.OnPointerClick(eventData);
        AudioManager.Instance.TriggerButtonClickSFX();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        foreach (var item in buttonModifiers) item.OnPointerEnter(eventData);
        if (button.interactable)
        {
            text.color = CustomDotTween.UpdateColor(Color.white);
            AudioManager.Instance.TriggerMouseSFX();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        foreach (var item in buttonModifiers) item.OnPointerExit(eventData);
        if (button.interactable) text.color = CustomDotTween.UpdateColor(Color.grey);
    }
    #endregion

    #region public functions
    public void RegisterOperation(ButtonDecorator operation) => buttonModifiers.Add(operation);
    #endregion

    #region private functions
    private void LogWarning(string msg) => Debug.LogWarning("[Button Component] : " + msg);
    #endregion
}
