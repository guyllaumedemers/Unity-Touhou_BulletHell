using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonComponent : IGraphicComponent
{
    public TextMeshProUGUI text { get; private set; }

    public RectTransform rect { get; private set; }

    public Button button { get; private set; }

    private void Awake()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
        rect = GetComponent<RectTransform>();
        button = GetComponent<Button>();


        if (!rect)
        {
            LogWarning($"This gameobject {gameObject.name} is not a UI element");
            return;
        }
        else if (!button)
        {
            LogWarning($"There is no button attach to this gameobject {gameObject.name}");
            return;
        }
        else if (!text)
        {
            LogWarning($"There is no text component attach to this gameobject {gameObject.name}");
            return;
        }
    }

    private void Start()
    {
        if (!button.tag.Equals(Globals.onStartupDefault) && !button.tag.Equals(Globals.on) && !button.tag.Equals(Globals.off))
        {
            button.colors = CustomDotTween.UpdateColorBlock(button.colors, Color.grey, Color.white, Color.grey);
        }
        else
        {
            button.colors = CustomDotTween.UpdateColorBlock(button.colors);
            if (button.tag.Equals(Globals.off))
            {
                // Toggle of color is set in the OSButtonManager as an event
                text.color = Color.grey;
            }
        }
    }

    #region public functions

    #region events

    public void OnPointerEnterSFX()
    {
        if (!button.interactable)
        {
            LogWarning($"Button {gameObject.name} interactable feature is set to false");
            return;
        }
        AudioManager.Instance.TriggerMouseSFX();
    }
    public void OnPointerClickSFX()
    {
        if (!button.interactable)
        {
            LogWarning($"Button {gameObject.name} interactable feature is set to false");
            return;
        }
        AudioManager.Instance.TriggerButtonClickSFX();
    }

    #endregion

    public override void PlayGraphicAnimation()
    {
        // do button behaviour
    }

    #endregion

    #region private functions

    private void LogWarning(string msg) => Debug.LogWarning("[Button Component] : " + msg);

    #endregion
}
