using UnityEngine;
using UnityEngine.UI;

public class MenuScreenHandler : AbsSceneHandler
{
    private void Awake() => PreIntilizationMethod();

    private void Start()
    {
        InitializationMethod(Globals.shortFadingTime, FindObjectsOfType<Button>(true));
    }

    protected override void PreIntilizationMethod()
    {
        base.PreIntilizationMethod();
        UIManager.Instance.PreInitializeUIManager();
        AudioManager.Instance.PreInitializeMenuScreen();
    }
    protected override void InitializationMethod(float fadeTime, params Button[] buttons)
    {
        base.InitializationMethod(fadeTime, buttons);
        UIManager.Instance.InitializeUIManager();
    }
}
