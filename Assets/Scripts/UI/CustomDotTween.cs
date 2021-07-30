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

    public static IEnumerator SlidingUI(RectTransform rect, float start, float end, float animationTime)
    {
        float time = 0.0f;
        float dir = end - start > 0.0f ? 1.0f : -1.0f;
        Debug.Log($"{rect.position.x } {end}");
        float ix = rect.position.x + (end - start);
        while (time < animationTime)
        {
            time += Time.deltaTime;
            rect.position += (new Vector3(Mathf.Lerp(start, end, time / animationTime), 0.0f, 0.0f) * dir);
            yield return new WaitForEndOfFrame();
        }
        rect.position = new Vector3(ix, rect.position.y, rect.position.z);
    }

    #endregion

    #region private functions

    private static Color UpdateColor(Color target) => new Color(target.r, target.g, target.b, target.a);

    #endregion
}
