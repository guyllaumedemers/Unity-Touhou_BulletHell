using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EntryPoint : SingletonMonoPersistent<EntryPoint>
{
    private EntryPoint() { }
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
        int last = curr_scene;
        this.EnsureRoutineStop(ref routine);
        this.CreateAnimationRoutine(Globals.sceneDelay, delegate (float progress) { }, delegate { StartCoroutine(LoadScene(++curr_scene)); });
    }

    public void TriggerPreviousScene()
    {
        int last = curr_scene;
        this.EnsureRoutineStop(ref routine);
        this.CreateAnimationRoutine(Globals.sceneDelay, delegate (float progress) { }, delegate { StartCoroutine(LoadScene(--curr_scene)); });
    }

    #endregion


    #region private functions

    private IEnumerator LoadScene(int index)
    {
        if (index < 0)
        {
            LogWarning("Scene Index invalid");
            yield return null;
        }
        SceneManager.LoadSceneAsync(index, LoadSceneMode.Single);
    }

    private void LogWarning(string msg) => Debug.LogWarning("[EntryPoint] : " + msg);

    #endregion
}
