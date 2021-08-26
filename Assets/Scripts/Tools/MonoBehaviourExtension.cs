using System;
using UnityEngine;

public static class MonoBehaviourExtension
{
    public static void EnsureRoutineStop(this MonoBehaviour value, ref Coroutine routine)
    {
        if (routine != null)
        {
            value.StopCoroutine(routine);
            routine = null;
        }
    }

    public static Coroutine CreateAnimationRoutine(this MonoBehaviour value, float duration, Action<float> changeFunction, Action onComplete = null)
    {
        return value.StartCoroutine(CustomDotTween.GenericAnimationRoutine(duration, changeFunction, onComplete));
    }
}
