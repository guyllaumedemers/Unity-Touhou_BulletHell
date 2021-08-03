using UnityEngine;
using UnityEngine.UI;

public class TitleScreenBehaviour : AbsSceneHandler
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
        SetStartButton(ref startButton);
        Load(ref alphagroup);
        AudioManager.Instance.PreInitializeTitleScreen();
    }

    private void Start()
    {
        Play();
        AudioManager.Instance.InitializeTitleScreen();
    }

    #region private functions

    private void SetStartButton(ref Button start)
    {
        start = FindObjectOfType<Button>();
        if (!start)
        {
            LogWarning("There is no button in the scene");
            return;
        }
        start.onClick.AddListener(() =>
        {
            this.EnsureRoutineStop(ref routine);
            this.CreateAnimationRoutine(Globals.fadingTime, delegate (float progress)
            {
                float ease = EasingFunction.EaseInOutExpo(0, 1, progress);
                alphagroup.alpha = Mathf.Lerp(0, 1, ease);
            },
            delegate { EntryPoint.Instance.TriggerNextScene(); }
            );
        });
    }

    private void LogWarning(string msg) => Debug.Log("[Title Screen Behaviour] : " + msg);

    #endregion
}
