using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class AbsSceneHandler : MonoBehaviour, ISceneHandler
{
    protected CanvasGroup alphagroup;
    protected Coroutine routine;

    public void Load(ref CanvasGroup canvasAlpha)
    {
        canvasAlpha = FindObjectOfType<CanvasGroup>();
        if (!canvasAlpha)
        {
            LogWarning("There is no cnavas group in the scene : " + SceneManager.GetActiveScene().name);
            return;
        }
        canvasAlpha.alpha = 1.0f;
        canvasAlpha.blocksRaycasts = false;
    }

    public virtual void Play()
    {
        if (!alphagroup)
        {
            LogWarning("The canvas group was not loaded : " + SceneManager.GetActiveScene().name);
            return;
        }
        this.EnsureRoutineStop(ref routine);
        this.CreateAnimationRoutine(Globals.curtainfade / 2, delegate (float progress)
        {
            float ease = EasingFunction.EaseInOutExpo(0, 1, progress);
            alphagroup.alpha = Mathf.Lerp(1, 0, ease);
        });
    }

    #region private functions

    private void LogWarning(string msg) => Debug.Log("[Abs Scene Behaviour] : " + msg);

    #endregion
}
