using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameScreenHandler : AbsSceneHandler
{
    private PlayerController player = null;
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
        ParticleSystem particule = FindObjectOfType<ParticleSystem>();
        if (!particule)
        {
            LogWarning("There is no particule system in the scene");
            return;
        }
        Destroy(particule.gameObject);
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
        player = FindObjectOfType<PlayerController>();
        if (!player)
        {
            player = Resources.LoadAll<PlayerController>(Globals.unitsPrefabs).FirstOrDefault();
            Instantiate<GameObject>(player.gameObject, FindObjectsOfType<Canvas>().Where(x => x.tag.Equals(Globals.gamecamera)).FirstOrDefault().transform);
        }

        FactoryManager.Instance.PreInitializeFactoryPrefabs();
        player.PreIntilizationPlayerController();
        ObjectPoolController.Instance.PreInitializeObjectPollController();
        CollisionController.Instance.PreInitializeCollisionController();
        BulletManager.Instance.PreInitializeBulletManager();
        UnitManager.Instance.PreInitializeUnitManager();
    }
    private void GameLogicInitialization()
    {
        FactoryManager.Instance.PreInitializeFactoryPrefabs();
        player.InitializationPlayerController();
    }
    private void UpdateGameLogic()
    {
        player.UpdatePlayerController();
        CollisionController.Instance.UpdateCollisionController(player);
        BulletManager.Instance.UpdateBulletManager();
        UnitManager.Instance.UpdateUnitManager();
    }
    private void LogWarning(string msg) => Debug.LogWarning("[Game Screen Behaviour] : " + msg);
    #endregion
}
