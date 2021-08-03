using UnityEngine;

public interface ISceneHandler
{
    public abstract void Load(ref CanvasGroup canvasAlpha);

    public abstract void Play();
}
