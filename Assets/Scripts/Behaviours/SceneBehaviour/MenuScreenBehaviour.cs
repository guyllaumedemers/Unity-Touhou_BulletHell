
public class MenuScreenBehaviour : AbsSceneHandler
{
    private void Awake()
    {
        Load(ref alphagroup);
        UIManager.Instance.PreIntilizationMethod();
        AudioManager.Instance.PreInitializeMenuScreen();
    }

    private void Start()
    {
        Play();
        UIManager.Instance.InitializationMethod();
    }
}
