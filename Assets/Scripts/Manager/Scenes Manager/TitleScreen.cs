
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TitleScreen : SingletonMono<TitleScreen>
{
    private CanvasGroup[] alphagroup;
    private Button button;
    private CanvasGroup first;
    private Coroutine routine;

    private void Awake()
    {
        alphagroup = FindObjectsOfType<CanvasGroup>();
        button = FindObjectOfType<Button>();
        if (alphagroup.Length < 2 || !button)
        {
            LogWarning($"You are missing canvas group component {alphagroup.Length} OR a button {button} in the scene");
            return;
        }
        AudioManager.Instance.PreInitializeTitleScreen();                                       // I dont like initializing here
        button.onClick.AddListener(EntryPoint.Instance.TriggerNextScene);

        foreach (var item in alphagroup)
        {
            item.alpha = 1;
            item.blocksRaycasts = false;
        }
        first = alphagroup.Where(x => x.tag == Globals.textcurtain).FirstOrDefault();
    }

    private void Start()
    {
        AudioManager.Instance.InitializeTitleScreen();                                          // same

        this.EnsureRoutineStop(ref routine);
        this.CreateAnimationRoutine(Globals.curtainfade, delegate (float progress)
        {
            float ease = EasingFunction.EaseInOutExpo(0, 1, progress);
            first.alpha = Mathf.Lerp(1, 0, ease);
        },
        delegate
        {
            first = alphagroup.Where(x => x.tag == Globals.uicurtain).FirstOrDefault();
            this.EnsureRoutineStop(ref routine);
            this.CreateAnimationRoutine(Globals.fadingTime * 2, delegate (float progress)
            {
                float ease = EasingFunction.EaseInOutExpo(0, 1, progress);
                first.alpha = Mathf.Lerp(1, 0, ease);
            });
        });
    }

    #region private functions

    private void LogWarning(string msg) => Debug.LogWarning("[TitleScreen] : " + msg);

    #endregion
}
