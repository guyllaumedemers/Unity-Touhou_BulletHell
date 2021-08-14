using UnityEngine;
using UnityEngine.UI;

public interface ISceneHandler
{
    public abstract void LoadCanvas();

    public abstract void PlayFadeAnimation(params Button[] buttons);
}
