using TMPro;
using UnityEngine;

public class ButtonComponent : IGraphicComponent
{
    public TextMeshProUGUI text { get; private set; }

    public RectTransform rect { get; private set; }

    private void Awake()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
        rect = GetComponent<RectTransform>();
    }

    #region public functions

    #region events

    public void OnPointerEnterSFX() => AudioManager.Instance.TriggerMouseSFX();
    public void OnPointerClickSFX() => AudioManager.Instance.TriggerButtonClickSFX();

    #endregion

    public override void PlayGraphicAnimation()
    {
        // do button behaviour
    }

    #endregion
}
