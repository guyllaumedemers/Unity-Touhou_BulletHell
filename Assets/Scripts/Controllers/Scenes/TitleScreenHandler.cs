using UnityEngine;
using UnityEngine.UI;

public class TitleScreenHandler : AbsSceneHandler
{
    private ParticleSystem particule;

    private void Awake()
    {
        particule = FindObjectOfType<ParticleSystem>(true);
        if (!particule)
        {
            LogWarning("There is no particule system in the scene");
            return;
        }

        DontDestroyOnLoad(particule);
        PreIntilizationMethod();
    }

    private void Start() => InitializationMethod(Globals.longFadingTime, SetStartButton());

    protected override void PreIntilizationMethod()
    {
        base.PreIntilizationMethod();
        AudioManager.Instance.PreInitializeTitleScreen();
    }

    protected override void InitializationMethod(float fadeTime, params Button[] buttons)
    {
        base.InitializationMethod(fadeTime, buttons);
        AudioManager.Instance.InitializeTitleScreen();
    }

    #region private functions
    private Button SetStartButton()
    {
        Button button = FindObjectOfType<Button>(true);
        if (!button)
        {
            LogWarning("There is no button in the scene");
            return null;
        }
        button.onClick.AddListener(() =>
        {
            AudioManager.Instance.TriggerButtonClickSFX();
            this.EnsureRoutineStop(ref routine);
            this.CreateAnimationRoutine(Globals.shortFadingTime, delegate (float progress)
            {
                float ease = EasingFunction.EaseInOutExpo(0, 1, progress);
                alphagroup.alpha = Mathf.Lerp(0, 1, ease);
            },
            delegate
            {
                SceneController.Instance.TriggerNextScene(Globals.sceneDelay);
            });
        });
        return button;
    }
    private void LogWarning(string msg) => Debug.LogWarning("[Title Screen Behaviour] : " + msg);
    #endregion
}
