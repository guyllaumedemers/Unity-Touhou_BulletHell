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
            text.color = UpdateColor(next ? Color.gray : Color.white);
            yield return new WaitForSeconds(animation / blinkPerSecond);
            next = !next;
        }
        text.color = Color.white;
    }

    public static IEnumerator WidgetUI(RectTransform rect, float animation)
    {
        float ix = rect.anchoredPosition.x;
        float time = 0.0f;
        float dir = 1;
        while (time < animation)
        {
            time += Time.deltaTime;
            rect.anchoredPosition = new Vector2(ix + Mathf.PingPong(Time.time, 10.0f) * dir, 0.0f);
            yield return null;
            dir *= -1;
        }
        rect.anchoredPosition = new Vector2(ix, rect.anchoredPosition.y);
    }

    #endregion

    #region private functions

    private static Color UpdateColor(Color target) => new Color(target.r, target.g, target.b, target.a);

    #endregion
}
