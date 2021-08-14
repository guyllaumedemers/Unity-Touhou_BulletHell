using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public static class CustomDotTween
{
    #region public functions

    public static IEnumerator BlinkingTextUI(TextMeshProUGUI text, float duration, int blinkPerSecond)
    {
        float time = 0.0f;
        bool next = true;
        while (time < duration)
        {
            time += Time.deltaTime;
            text.color = UpdateColor(next ? Color.grey : Color.white);
            yield return new WaitForSeconds(duration / blinkPerSecond);
            next = !next;
        }
        text.color = Color.white;
    }

    public static IEnumerator BlinkingImgUI(Image img, float duration, int blinkPerSecond)
    {
        float time = 0.0f;
        bool next = true;
        while (time < duration)
        {
            time += Time.deltaTime;
            img.color = UpdateColor(next ? Color.grey : Color.white);
            yield return new WaitForSeconds(duration / blinkPerSecond);
            next = !next;
        }
        img.color = Color.white;
    }

    public static IEnumerator BuzzingUI(RectTransform rect, float duration)
    {
        float ix = rect.anchoredPosition.x;
        float time = 0.0f;
        float dir = 1.0f;
        while (time < duration)
        {
            time += Time.deltaTime;
            rect.anchoredPosition = new Vector2(ix + Mathf.PingPong(Time.time, 10.0f) * dir, 0.0f);
            yield return new WaitForEndOfFrame();
            dir *= -1.0f;
        }
        rect.anchoredPosition = new Vector2(ix, rect.anchoredPosition.y);
    }

    public static IEnumerator StaircaseAnimation(RectTransform[] buttonsRect, float duration, Action<RectTransform, float> next)
    {
        foreach (var item in buttonsRect)
        {
            next.Invoke(item, duration);

            float time = 0.0f;
            while (time < duration / buttonsRect.Length)
            {
                time += Time.deltaTime;
                yield return null;
            }
        }
    }

    public static IEnumerator SlideAnimation(RectTransform rect, float endAnchorpos, float duration)
    {
        Vector2 start = rect.anchoredPosition;
        Vector2 end = new Vector2(endAnchorpos, rect.anchoredPosition.y);
        float time = 0.0f;
        while (time < duration)
        {
            float ease = EasingFunction.EaseInOutSine(0, 1, time / duration);
            rect.anchoredPosition = Vector2.Lerp(start, end, ease);
            time += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        rect.anchoredPosition = end;
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
        if (colors.Length < 3)
        {
            LogWarning("Color params is missing and cannot assign colors to the normal color and highlighted color, Default ColorBlock returned");
            return ColorBlock.defaultColorBlock;
        }
        cb.normalColor = colors[0];
        cb.highlightedColor = colors[1];
        cb.selectedColor = colors[2];
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
