
public class MenuScreenBehaviour : AbsSceneHandler
{
    private void Awake()
    {
        LoadCanvas(ref alphagroup);
        UIManager.Instance.PreIntilizationMethod();
        AudioManager.Instance.PreInitializeMenuScreen();
    }

    private void Start()
    {
        PlayFadeAnimation();
        UIManager.Instance.InitializationMethod();
    }
}
