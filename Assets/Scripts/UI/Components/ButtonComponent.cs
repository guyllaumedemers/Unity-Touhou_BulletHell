using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonComponent : IGraphicComponent
{
    public TextMeshProUGUI text { get; private set; }

    public RectTransform rect { get; private set; }

    private Button instance;

    private void Awake()
    {
        instance = GetComponent<Button>();
        rect = GetComponent<RectTransform>();
        text = GetComponentInChildren<TextMeshProUGUI>();

        if (!instance || !text || !rect)
        {
            LogWarning($"There's a component missing : text {text} rect {rect} button {instance}");
            return;
        }
    }

    private void Start()
    {
        if (!instance.tag.Equals(Globals.onStartupDefault) && !instance.tag.Equals(Globals.on) && !instance.tag.Equals(Globals.off))
        {
            instance.colors = CustomDotTween.UpdateColorBlock(instance.colors, Color.grey, Color.white);
        }
        else
        {
            instance.colors = CustomDotTween.UpdateColorBlock(instance.colors, Color.white, Color.white);
            if (instance.tag.Equals(Globals.off))
            {
                text.color = Color.grey;
            }
        }
    }

    #region public functions

    #region events

    public void OnPointerEnterSFX()
    {
        if (!instance.interactable)
        {
            LogWarning($"Button {gameObject.name} interactable feature is set to false");
            return;
        }
        AudioManager.Instance.TriggerMouseSFX();
    }
    public void OnPointerClickSFX()
    {
        if (!instance.interactable)
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
