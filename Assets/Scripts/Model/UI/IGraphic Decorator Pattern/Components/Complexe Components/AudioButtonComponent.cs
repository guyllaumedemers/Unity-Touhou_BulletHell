using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AudioButtonComponent : ButtonComponent
{
    Image img;

    protected override void Awake()
    {
        this.button = GetComponent<Button>();
        this.img = GetComponent<Image>();

        if (!button)
        {
            LogWarning("There is no button component attach to this gameobject " + gameObject.name);
            return;
        }
        else if (!img)
        {
            LogWarning("There is no image component attach to this gameobject " + gameObject.name);
            return;
        }

        this.button.colors = CustomDotTween.UpdateColorBlock(button.colors, Color.grey, Color.white, Color.grey);
        this.img.raycastTarget = true;
    }

    public override void OnPointerClick(PointerEventData eventData) => AudioManager.Instance.TriggerButtonClickSFX();

    public override void OnPointerEnter(PointerEventData eventData) => AudioManager.Instance.TriggerMouseSFX();

    public override void OnPointerExit(PointerEventData eventData) { }

    private void OnEnable() => img.raycastTarget = true;

    #region private functions
    private void LogWarning(string msg) => Debug.LogWarning("[Audio Button Component] : " + msg);
    #endregion
}
