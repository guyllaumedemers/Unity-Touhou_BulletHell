using UnityEngine;
using UnityEngine.UI;

public class MenuScreenHandler : AbsSceneHandler
{
    private void Awake() => PreIntilizationMethod();

    private void Start()
    {
        InitializationMethod(Globals.shortFadingTime, FindObjectsOfType<Button>(true));
    }

    #region override functions
    protected override void PreIntilizationMethod()
    {
        base.PreIntilizationMethod();
        UIManager.Instance.PreIntilizationMethod();
        AudioManager.Instance.PreInitializeMenuScreen();
    }
    protected override void InitializationMethod(float fadeTime, params Button[] buttons)
    {
        base.InitializationMethod(fadeTime, buttons);
        UIManager.Instance.InitializationMethod();
    }
    #endregion
}
