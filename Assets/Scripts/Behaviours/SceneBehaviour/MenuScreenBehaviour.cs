
public class MenuScreenBehaviour : AbsSceneHandler
{
    private void Awake()
    {
        LoadCanvas();
        UIManager.Instance.PreIntilizationMethod();
        AudioManager.Instance.PreInitializeMenuScreen();
    }

    private void Start()
    {
        PlayFadeAnimation();
        UIManager.Instance.InitializationMethod();
    }
}
