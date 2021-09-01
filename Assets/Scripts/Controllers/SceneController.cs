using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class SceneController : SingletonMonoPersistent<SceneController>
{
    private Coroutine routine;
    private GameObject[] managerprefabs;
    private int curr_scene = (int)SceneEnum.None;

    public override void Awake()
    {
        base.Awake();
        OnAwakeManagerPersistent();
        DontDestroyOnLoad(FindObjectOfType<CanvasGroup>().transform.parent);
        DontDestroyOnLoad(FindObjectOfType<ParticleSystem>());
        DontDestroyOnLoad(FindObjectOfType<EventSystem>());
        DontDestroyOnLoad(FindObjectOfType<Volume>());

        this.EnsureRoutineStop(ref routine);
        this.CreateAnimationRoutine(0f, delegate (float progress) { }, delegate { TriggerNextScene(); });
    }

    #region public functions
    public void TriggerNextScene(float delay = 0f)
    {
        this.EnsureRoutineStop(ref routine);
        this.CreateAnimationRoutine(delay, delegate (float progress) { }, delegate
        {
            StartCoroutine(LoadScene(++curr_scene));
        });
    }

    public void TriggerPreviousScene(float delay = 0f)
    {
        this.EnsureRoutineStop(ref routine);
        this.CreateAnimationRoutine(delay, delegate (float progress) { }, delegate
        {
            StartCoroutine(LoadScene(--curr_scene));
        });
    }
    #endregion


    #region private functions
    private IEnumerator LoadScene(int index)
    {
        if (index < 0)
        {
            LogWarning("Scene Index invalid");
            yield break;
        }
        AsyncOperation async = SceneManager.LoadSceneAsync(index, LoadSceneMode.Single);
        while (!async.isDone)
        {
            yield return null;
        }
        UpdateSceneHandlingScript(curr_scene);
        UpdateSceneCanvasCameras();
    }

    private void UpdateSceneHandlingScript(int index)
    {
        switch ((SceneEnum)index)
        {
            case SceneEnum.None:
                break;
            case SceneEnum.Title:
                gameObject.AddComponent<TitleScreenHandler>();
                break;
            case SceneEnum.Menu:
                Destroy(gameObject.GetComponent<TitleScreenHandler>());
                gameObject.AddComponent<MenuScreenHandler>();
                break;
            case SceneEnum.Game:
                Destroy(gameObject.GetComponent<MenuScreenHandler>());
                gameObject.AddComponent<GameScreenHandler>();
                break;
            default:
                throw new System.InvalidOperationException();
        }
    }

    private void UpdateSceneCanvasCameras()
    {
        Canvas[] arr = FindObjectsOfType<Canvas>();
        if (arr.Length < 1)
        {
            LogWarning("There is no canvas in this scene : " + SceneManager.GetActiveScene().name);
            return;
        }
        foreach (var item in arr.Where(x => x.gameObject.tag != Globals.gameview)) item.worldCamera = Camera.main;
        foreach (var item in arr.Where(x => x.gameObject.tag.Equals(Globals.gameview))) item.worldCamera = FindObjectsOfType<Camera>().Where(x => x.gameObject.tag.Equals(Globals.gameview)).FirstOrDefault();
    }

    private void OnAwakeManagerPersistent()
    {
        managerprefabs = Resources.LoadAll<GameObject>(Globals.managerPrefabs);
        if (managerprefabs.Length < 1)
        {
            LogWarning("Manager Ressources.LoadAll failed : " + managerprefabs.Length);
            return;
        }

        foreach (var item in managerprefabs)
        {
            if (item.GetComponent<AudioManager>())
            {
                AudioManager go = Utilities.InstanciateType<AudioManager>(item, null, Vector2.zero);
                DontDestroyOnLoad(go);
            }
            else
            {
                UIManager go = Utilities.InstanciateType<UIManager>(item, null, Vector2.zero);
                DontDestroyOnLoad(go);
            }
        }
    }

    private void LogWarning(string msg) => Debug.LogWarning("[Scene Controller] : " + msg);
    #endregion
}
