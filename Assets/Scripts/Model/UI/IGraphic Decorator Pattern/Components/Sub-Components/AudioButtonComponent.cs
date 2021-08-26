using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AudioButtonComponent : ButtonComponent
{
    protected override void Awake()
    {
        this.button = GetComponentInChildren<Button>();

        if (!button)
        {
            LogWarning("There is no text component attach to this gameobject " + gameObject.name);
            return;
        }

        button.colors = CustomDotTween.UpdateColorBlock(button.colors, Color.grey, Color.white, Color.grey);
    }
    public override void OnPointerEnter(PointerEventData eventData)
    {
        /*  The override is to avoid button color change
         *  
         */
        if (button.interactable)
        {
            AudioManager.Instance.TriggerMouseSFX();
        }
        foreach (var item in buttonModifiers) item.OnPointerEnter(eventData);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        foreach (var item in buttonModifiers) item.OnPointerExit(eventData);
    }

    #region private functions
    private void LogWarning(string msg) => Debug.LogWarning("[Audio Button Component] : " + msg);
    #endregion
}
