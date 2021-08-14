using UnityEngine;
using UnityEngine.UI;

public class PanelComponent : IGraphicComponent
{
    private Button[] buttons;

    private void Awake()
    {
        buttons = GetComponentsInChildren<Button>();
        if (buttons.Length <= 0)
        {
            LogWarning("There is no button child in this component");
            return;
        }
    }

    public override void PlayGraphicAnimation()
    {
        // do PanelComponent behaviours
    }

    #region private functions

    private void LogWarning(string msg) => Debug.LogWarning("[Panel Component] : " + msg);

    #endregion
}
