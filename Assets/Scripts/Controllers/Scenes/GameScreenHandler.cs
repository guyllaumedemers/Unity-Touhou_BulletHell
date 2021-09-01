using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameScreenHandler : AbsSceneHandler
{
    private PlayerController player = null;
    private float last;
    private bool canRunUpdate = false;
    private CanvasGroup loadingscreen;
    private Slider loadingslider;

    private void Awake()
    {
        loadingscreen = FindObjectsOfType<CanvasGroup>(true).Where(x => x.tag.Equals(Globals.gameview)).FirstOrDefault();
        loadingslider = FindObjectsOfType<Slider>(true).FirstOrDefault();
        if (!loadingscreen)
        {
            LogWarning("There is no canvas group with the tag : " + Globals.gameview);
            return;
        }
        else if (!loadingslider)
        {
            LogWarning("There is no slider with the tag : " + Globals.gameview);
            return;
        }
        loadingscreen.alpha = 1.0f;
        loadingscreen.blocksRaycasts = false;
        loadingscreen.interactable = false;
        loadingslider.value = 0.0f;
        PreIntilizationMethod();
    }

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
        ParticleSystem particule = FindObjectOfType<ParticleSystem>();
        if (!particule)
        {
            LogWarning("There is no particule system in the scene");
            return;
        }
        Destroy(particule.gameObject);
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
            PreGameLogicInitialization();
        });
    }

    #region private functions
    private void PreGameLogicInitialization()
    {
        player = FindObjectOfType<PlayerController>();
        if (!player)
        {
            player = Resources.LoadAll<PlayerController>(Globals.unitsPrefabs).FirstOrDefault();
            Instantiate<GameObject>(player.gameObject, FindObjectsOfType<Canvas>().Where(x => x.tag.Equals(Globals.gameview)).FirstOrDefault().transform);
        }

        Queue<Action> stack = new Queue<Action>();
        stack.Enqueue(() => { FactoryManager.Instance.PreInitializeFactoryPrefabs(); });
        stack.Enqueue(() => { player.PreIntilizationPlayerController(); });
        stack.Enqueue(() => { ObjectPoolController.Instance.PreInitializeObjectPoolController(); });
        stack.Enqueue(() => { CollisionController.Instance.PreInitializeCollisionController(); });
        stack.Enqueue(() => { BulletManager.Instance.PreInitializeBulletManager(); });
        stack.Enqueue(() => { UnitManager.Instance.PreInitializeUnitManager(); });

        StartCoroutine(FunctionLoader(stack));
    }
    private void GameLogicInitialization()
    {
        player.InitializationPlayerController();
        // Trigger Stage level animation and delay for wave
    }
    private void UpdateGameLogic()
    {
        player.UpdatePlayerController();
        CollisionController.Instance.UpdateCollisionController(player);
        BulletManager.Instance.UpdateBulletManager();
        UnitManager.Instance.UpdateUnitManager();
    }

    private IEnumerator FunctionLoader(Queue<Action> stack)
    {
        int count = stack.Count;
        while (stack.Count > 0)
        {
            Action action = stack.Dequeue();
            action.Invoke();
            loadingslider.value += 1.0f / count;
            yield return new WaitForSeconds(1.0f / count);
        }
        this.EnsureRoutineStop(ref routine);
        this.CreateAnimationRoutine(Globals.shortFadingTime, delegate (float progress)
        {
            float ease = EasingFunction.EaseInOutExpo(0, 1, progress);
            loadingscreen.alpha = Mathf.Lerp(1, 0, ease);
        },
        delegate
        {
            GameLogicInitialization();
            canRunUpdate = true;
        });
    }

    private void LogWarning(string msg) => Debug.LogWarning("[Game Screen Behaviour] : " + msg);
    #endregion
}
