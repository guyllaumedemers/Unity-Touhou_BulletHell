using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameScreenHandler : AbsSceneHandler
{
    private ParticleSystem particule;
    private float last;
    private bool canRunUpdate = false;

    private void Awake() => PreIntilizationMethod();

    private void Start() => InitializationMethod(Globals.shortFadingTime, null);

    private void Update()
    {
        if (!canRunUpdate)
        {
            return;
        }
        else if (Time.time - last >= Globals.fps)
        {
            UpdateGameLogic();
            last = Time.time;
        }
    }

    protected override void PreIntilizationMethod()
    {
        base.PreIntilizationMethod();
        particule = FindObjectOfType<ParticleSystem>();
        if (!particule)
        {
            LogWarning("There is no particule system in the scene");
            return;
        }
        Destroy(particule);
        PreGameLogicInitialization();
        last = Time.time;
    }

    protected override void InitializationMethod(float curtainFadeTime, params Button[] buttons)
    {
        if (!alphagroup)
        {
            LogWarning("The canvas group was not loaded : " + SceneManager.GetActiveScene().name);
            return;
        }
        else if (buttons != null && buttons.Length > 0)
        {
            foreach (var b in buttons) b.interactable = false;
        }

        this.EnsureRoutineStop(ref routine);
        this.CreateAnimationRoutine(curtainFadeTime, delegate (float progress)
        {
            float ease = EasingFunction.EaseInOutExpo(0, 1, progress);
            alphagroup.alpha = Mathf.Lerp(1, 0, ease);
        },
        delegate
        {
            GameLogicInitialization();
            canRunUpdate = true;
        });
    }

    #region private functions
    private void PreGameLogicInitialization()
    {
        FactoryManager.Instance.PreIntilizationMethod();
        ObjectPoolController.PreInitializeMethod();
        CollisionController.Instance.PreIntilizationMethod();
        PlayerController.Instance.PreIntilizationMethod();
        BulletManager.Instance.PreIntilizationMethod();
        UnitManager.Instance.PreIntilizationMethod();
    }
    private void GameLogicInitialization()
    {
        PlayerController.Instance.InitializationMethod();
        // Scene should be loaded in memory and the wave should start after a period of time once the scene is ready
        StartCoroutine(WaveController.Instance.StartWave(0, (int)DirectionEnum.None, (int)DirectionEnum.Pivot, 4));
    }
    private void UpdateGameLogic()
    {
        PlayerController.Instance.UpdateMethod();
        CollisionController.Instance.UpdateMethod();
        BulletManager.Instance.UpdateMethod();
        UnitManager.Instance.UpdateMethod();
    }
    private void LogWarning(string msg) => Debug.LogWarning("[Game Screen Behaviour] : " + msg);
    #endregion
}
