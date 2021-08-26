using UnityEngine;
using UnityEngine.UI;

public class TitleScreenHandler : AbsSceneHandler
{
    private Button startButton;
    private ParticleSystem particule;

    private void Awake()
    {
        particule = FindObjectOfType<ParticleSystem>();
        if (!particule)
        {
            LogWarning("There is no particule system in the scene");
            return;
        }
        DontDestroyOnLoad(particule);
        PreIntilizationMethod();
    }

    private void Start() => InitializationMethod(startButton);

    protected override void PreIntilizationMethod()
    {
        base.PreIntilizationMethod();
        startButton = SetStartButton(startButton);
        AudioManager.Instance.PreInitializeTitleScreen();
    }

    protected override void InitializationMethod(params Button[] buttons)
    {
        base.InitializationMethod(buttons);
        AudioManager.Instance.InitializeTitleScreen();
    }

    #region private functions
    private Button SetStartButton(Button button)
    {
        button = FindObjectOfType<Button>();
        if (!button)
        {
            LogWarning("There is no button in the scene");
            return null;
        }
        button.onClick.AddListener(() =>
        {
            AudioManager.Instance.TriggerButtonClickSFX();
            this.EnsureRoutineStop(ref routine);
            this.CreateAnimationRoutine(Globals.fadingTime, delegate (float progress)
            {
                float ease = EasingFunction.EaseInOutExpo(0, 1, progress);
                alphagroup.alpha = Mathf.Lerp(0, 1, ease);
            },
            delegate { SceneController.Instance.TriggerNextScene(Globals.sceneDelay); });
        });
        return button;
    }
    private void LogWarning(string msg) => Debug.LogWarning("[Title Screen Behaviour] : " + msg);
    #endregion
}
