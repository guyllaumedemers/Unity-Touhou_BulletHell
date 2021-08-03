using UnityEngine;

public class MenuScreen : SingletonMono<MenuScreen>
{
    private CanvasGroup[] alphagroup;
    private CanvasGroup first;
    private Canvas maincanvas;
    private Coroutine routine;

    private void Awake()
    {
        UITool.SetUI(ref maincanvas, ref alphagroup, ref first, Globals.uicurtain);

        UIManager.Instance.PreIntilizationMethod();
        AudioManager.Instance.PreInitializeMenuScreen();
    }

    private void Start()
    {
        UIManager.Instance.InitializationMethod();
        this.EnsureRoutineStop(ref routine);
        this.CreateAnimationRoutine(Globals.curtainfade, delegate (float progress)
        {
            float ease = EasingFunction.EaseInOutExpo(0, 1, progress);
            first.alpha = Mathf.Lerp(1, 0, ease);
        });
    }

    private void LogWarning(string msg) => Debug.LogWarning("[MenuScreen] : " + msg);
}
