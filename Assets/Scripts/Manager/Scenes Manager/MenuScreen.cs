using UnityEngine;

public class MenuScreen : SingletonMono<MenuScreen>
{
    private CanvasGroup alphagroup;
    private Coroutine routine;

    private void Awake()
    {
        alphagroup = FindObjectOfType<CanvasGroup>();
        if (!alphagroup)
        {
            LogWarning("There is no canvas group in the scene");
            return;
        }
        alphagroup.alpha = 1;
        alphagroup.blocksRaycasts = false;

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
            alphagroup.alpha = Mathf.Lerp(1, 0, ease);
        });
    }

    private void LogWarning(string msg) => Debug.LogWarning("[MenuScreen] : " + msg);
}
