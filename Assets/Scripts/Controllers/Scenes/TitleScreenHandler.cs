using UnityEngine;
using UnityEngine.UI;

public class TitleScreenHandler : AbsSceneHandler
{
    private void Awake() => PreIntilizationMethod();

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
}
