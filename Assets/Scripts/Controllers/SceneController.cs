using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : SingletonMonoPersistent<SceneController>
{
    private SceneController() { }
    private AbsSceneHandler sceneHandler;
    private Coroutine routine;
    private int curr_scene = (int)SceneEnum.None;

    public override void Awake()
    {
        base.Awake();
        this.EnsureRoutineStop(ref routine);
        this.CreateAnimationRoutine(Globals.sceneDelay, delegate (float progress) { }, delegate { TriggerNextScene(); });
    }

    #region public functions
    public void TriggerNextScene()
    {
        this.EnsureRoutineStop(ref routine);
        this.CreateAnimationRoutine(Globals.sceneDelay, delegate (float progress) { }, delegate
        {
            StartCoroutine(LoadScene(++curr_scene));
        });
    }

    public void TriggerPreviousScene()
    {
        this.EnsureRoutineStop(ref routine);
        this.CreateAnimationRoutine(Globals.sceneDelay, delegate (float progress) { }, delegate
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
        sceneHandler = UpdateSceneHandlingScript(curr_scene);
    }

    private AbsSceneHandler UpdateSceneHandlingScript(int index)
    {
        switch ((SceneEnum)index)
        {
            case SceneEnum.None:
                return null;
            case SceneEnum.Title:
                // no script to delete
                return gameObject.AddComponent<TitleScreenHandler>();
            case SceneEnum.Menu:
                Destroy(gameObject.GetComponent<TitleScreenHandler>());
                return gameObject.AddComponent<MenuScreenHandler>();
            case SceneEnum.Game:
                Destroy(gameObject.GetComponent<MenuScreenHandler>());
                return gameObject.AddComponent<GameScreenHandler>();
            default:
                throw new System.InvalidOperationException();
        }
    }

    private void LogWarning(string msg) => Debug.LogWarning("[Scene Controller] : " + msg);
    #endregion
}
