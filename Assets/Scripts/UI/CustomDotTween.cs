using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public static class CustomDotTween
{
    #region public functions

    public static IEnumerator BlinkingTextUI(TextMeshProUGUI text, float animation, int blinkPerSecond)
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

    public static IEnumerator BlinkingImgUI(Image img, float animation, int blinkPerSecond)
    {
        float time = 0.0f;
        bool next = true;
        while (time < animation)
        {
            time += Time.deltaTime;
            img.color = UpdateColor(next ? Color.grey : Color.white);
            yield return new WaitForSeconds(animation / blinkPerSecond);
            next = !next;
        }
        img.color = Color.white;
    }

    public static IEnumerator BuzzingUI(RectTransform rect, float animation)
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

    public static ColorBlock UpdateColorBlock(ColorBlock cb, params Color[] colors)
    {
        if (colors.Length < 2)
        {
            LogWarning("Color params is missing and cannot assign colors to the normal color and highlighted color, Default ColorBlock returned");
            return ColorBlock.defaultColorBlock;
        }
        cb.normalColor = colors[0];
        cb.highlightedColor = colors[1];
        return cb;
    }

    public static void ToggleTextColor(TextMeshProUGUI src, TextMeshProUGUI target)
    {
        Color temp = src.color;
        src.color = target.color;
        target.color = temp;
    }

    #endregion

    #region private functions

    private static Color UpdateColor(Color target) => new Color(target.r, target.g, target.b, target.a);

    private static void LogWarning(string msg) => Debug.LogWarning("[CustomDotTween] : " + msg);

    #endregion
}
