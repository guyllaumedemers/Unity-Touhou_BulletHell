using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public abstract class AbsSceneHandler : MonoBehaviour, ISceneHandler
{
    protected CanvasGroup alphagroup;
    protected Coroutine routine;

    public void LoadCanvas()
    {
        alphagroup = FindObjectOfType<CanvasGroup>(true);
        if (!alphagroup)
        {
            LogWarning("There is no canvas group in the scene : " + SceneManager.GetActiveScene().name);
            return;
        }
        InitializeCanvasToDefaultValues();
    }

    public virtual void PlayFadeAnimation(params Button[] buttons)
    {
        if (!alphagroup)
        {
            LogWarning("The canvas group was not loaded : " + SceneManager.GetActiveScene().name);
            return;
        }
        else if (buttons.Length > 0)
        {
            foreach (var b in buttons) b.interactable = false;
        }

        this.EnsureRoutineStop(ref routine);
        this.CreateAnimationRoutine(Globals.curtainfade / 2, delegate (float progress)
        {
            float ease = EasingFunction.EaseInOutExpo(0, 1, progress);
            alphagroup.alpha = Mathf.Lerp(1, 0, ease);
        },
        delegate
        {
            if (buttons.Length != 0)
            {
                foreach (var b in buttons) b.interactable = true;
            }
        });
    }

    #region private functions

    private void InitializeCanvasToDefaultValues()
    {
        alphagroup.gameObject.SetActive(true);
        alphagroup.alpha = 1.0f;
        alphagroup.blocksRaycasts = false;
    }

    private void LogWarning(string msg) => Debug.LogWarning("[Abs Scene Behaviour] : " + msg);

    #endregion
}
