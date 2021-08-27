using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : SingletonMonoPersistent<SceneController>
{
    private SceneController() { }
    private Coroutine routine;
    private int curr_scene = (int)SceneEnum.None;

    public override void Awake()
    {
        base.Awake();
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

    private void LogWarning(string msg) => Debug.LogWarning("[Scene Controller] : " + msg);
    #endregion
}
