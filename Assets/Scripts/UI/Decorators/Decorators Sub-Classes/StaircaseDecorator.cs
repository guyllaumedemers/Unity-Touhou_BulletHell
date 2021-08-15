using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class StaircaseDecorator : PanelDecorator
{
    public RectTransform[] rects { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        rects = GetComponentsInChildren<Button>().Select(x => x.GetComponent<RectTransform>()).ToArray();
        if (rects.Length < 1)
        {
            LogWarning($"There is no button child on this component {gameObject.name}");
            return;
        }
    }

    public override void PlayGraphicAnimation()
    {
        base.PlayGraphicAnimation();
        StaircaseAnimation();
    }

    #region private functions

    private void StaircaseAnimation()
    {
        StopCoroutine(typeof(CustomDotTween).GetMethods().Where(x => x.Name == "StaircaseAnimation").FirstOrDefault().Name);
        StartCoroutine(CustomDotTween.StaircaseAnimation(rects, Globals.staircaseTime,
            delegate (RectTransform curr, float duration)
            {
                StartCoroutine(CustomDotTween.SlideAnimation(curr, panelInstance.anchposX - Globals.sliding_offset, duration));
            }
            ));
    }

    private void OnEnable()
    {
        if (rects.Length < 1)
        {
            LogWarning("OnEnable function called on uninitialized rect transform array");
            return;
        }
        foreach (var item in rects)
        {
            item.anchoredPosition = new Vector2(panelInstance.anchposX, item.anchoredPosition.y);
        }
    }

    private void LogWarning(string msg) => Debug.LogWarning("[Staircase Decorator] : " + msg);

    #endregion
}
