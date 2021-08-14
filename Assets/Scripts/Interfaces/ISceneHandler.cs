using UnityEngine;
using UnityEngine.UI;

public interface ISceneHandler
{
    public abstract void LoadCanvas(ref CanvasGroup canvasAlpha);

    public abstract void PlayFadeAnimation(params Button[] buttons);
}
