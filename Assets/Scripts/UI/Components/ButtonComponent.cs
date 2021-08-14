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
        text = GetComponentInChildren<TextMeshProUGUI>();
        rect = GetComponent<RectTransform>();
        instance = GetComponent<Button>();
    }

    private void Start()
    {
        instance.colors = CustomDotTween.UpdateColorBlock(instance.colors, Color.grey, Color.white);
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
