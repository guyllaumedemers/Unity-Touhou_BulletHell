using System;
using System.Collections;
using TMPro;
using UnityEngine;

public static class CustomDotTween
{
    #region public functions

    public static IEnumerator BlinkingUI(TextMeshProUGUI text, float animation, int blinkPerSecond)
    {
        float time = 0.0f;
        bool next = true;
        while (time < animation)
        {
            time += Time.deltaTime;
            text.color = UpdateColor(next ? Color.grey : Color.white);
            yield return new WaitForSeconds(animation / blinkPerSecond);
            next = !next;
        }
        text.color = Color.white;
    }

    public static IEnumerator WidgetUI(RectTransform rect, float animation)
    {
        float ix = rect.anchoredPosition.x;
        float time = 0.0f;
        float dir = 1.0f;
        while (time < animation)
        {
            time += Time.deltaTime;
            rect.anchoredPosition = new Vector2(ix + Mathf.PingPong(Time.time, 10.0f) * dir, 0.0f);
            yield return new WaitForEndOfFrame();
            dir *= -1.0f;
        }
        rect.anchoredPosition = new Vector2(ix, rect.anchoredPosition.y);
    }

    #endregion

    #region Testing

    public static IEnumerator GenericAnimationRoutine(float duration, Action<float> changeFunction, Action onComplete)
    {
        float elapsedTime = 0.0f;
        float progress = 0.0f;
        while (progress < 1.0f)
        {
            changeFunction(progress);
            elapsedTime += Time.deltaTime;
            progress = elapsedTime / duration;
            yield return new WaitForEndOfFrame();
        }
        changeFunction(1.0f);
        onComplete?.Invoke();
    }

    #endregion

    #region private functions

    private static Color UpdateColor(Color target) => new Color(target.r, target.g, target.b, target.a);

    #endregion
}
