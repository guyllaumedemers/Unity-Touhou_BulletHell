
using UnityEngine.UI;

public class MenuScreenHandler : AbsSceneHandler
{
    private void Awake() => PreIntilizationMethod();

    private void Start() => InitializationMethod();

    #region override functions
    protected override void PreIntilizationMethod()
    {
        base.PreIntilizationMethod();
        UIManager.Instance.PreIntilizationMethod();
        AudioManager.Instance.PreInitializeMenuScreen();
    }
    protected override void InitializationMethod(params Button[] buttons)
    {
        base.InitializationMethod(buttons);
        UIManager.Instance.InitializationMethod();
    }
    #endregion
}
