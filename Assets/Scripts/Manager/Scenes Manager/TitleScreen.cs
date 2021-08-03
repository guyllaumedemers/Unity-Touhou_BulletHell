using UnityEngine;
using UnityEngine.UI;

public class TitleScreen : SingletonMono<TitleScreen>
{
    private CanvasGroup[] alphagroup;
    private Button button;
    private CanvasGroup first;
    private Canvas maincanvas;
    private Coroutine routine;

    private void Awake()
    {
        SetButtonScene();
        UITool.SetUI(ref maincanvas, ref alphagroup, ref first, Globals.textcurtain);
        AudioManager.Instance.PreInitializeTitleScreen();                                       // I dont like initializing here
    }

    private void Start()
    {
        AudioManager.Instance.InitializeTitleScreen();                                          // same
        RunAnimation();
    }

    #region private functions

    private void SetButtonScene()
    {
        button = FindObjectOfType<Button>();
        if (!button)
        {
            LogWarning($"You are missing canvas group component {alphagroup.Length} OR a button {button} in the scene");
            return;
        }
        button.onClick.AddListener(EntryPoint.Instance.TriggerNextScene);
    }

    private void RunAnimation()
    {
        this.EnsureRoutineStop(ref routine);
        this.CreateAnimationRoutine(Globals.curtainfade, delegate (float progress)
        {
            float ease = EasingFunction.EaseInOutExpo(0, 1, progress);
            first.alpha = Mathf.Lerp(1, 0, ease);
        });
    }

    private void LogWarning(string msg) => Debug.LogWarning("[TitleScreen] : " + msg);

    #endregion
}
