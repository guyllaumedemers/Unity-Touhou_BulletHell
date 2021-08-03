using UnityEngine;
using UnityEngine.SceneManagement;

public class EntryPoint : SingletonMonoPersistent<EntryPoint>
{
    private EntryPoint() { }
    private int curr_scene = (int)SceneEnum.TitleScreen;

    public override void Awake()
    {
        base.Awake();
        LoadScene(curr_scene);
    }

    #region public functions

    public void TriggerNextScene()
    {
        int last = curr_scene;
        LoadScene(++curr_scene, last);
    }

    public void TriggerPreviousScene()
    {
        int last = curr_scene;
        LoadScene(--curr_scene, last);
    }

    #endregion


    #region private functions

    private void LoadScene(int index, int last = default)
    {
        if (index < 0)
        {
            LogWarning("Scene Index invalid");
            return;
        }
        else if (last != 0 && last > 0)
        {
            SceneManager.UnloadSceneAsync(last);
        }
        SceneManager.LoadSceneAsync(index);
    }

    private void LogWarning(string msg) => Debug.LogWarning("[EntryPoint] : " + msg);

    #endregion
}
