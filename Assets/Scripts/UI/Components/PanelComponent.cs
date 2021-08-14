using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PanelComponent : IGraphicComponent
{
    public RectTransform[] rects { get; private set; }
    public RectTransform instance { get; private set; }
    public float anchpos { get; private set; }

    private void Awake()
    {
        rects = GetComponentsInChildren<Button>().Select(x => x.GetComponent<RectTransform>()).ToArray();
        instance = GetComponent<RectTransform>();

        if (!instance)
        {
            LogWarning($"The gameobject {gameObject.name} is not a UI element");
            return;
        }
        else if (rects.Length < 1)
        {
            LogWarning("There is no button child on this component");
            return;
        }
        anchpos = instance.anchoredPosition.x;
    }

    public override void PlayGraphicAnimation()
    {
        // do PanelComponent behaviours
    }

    #region private functions

    private void LogWarning(string msg) => Debug.LogWarning("[Panel Component] : " + msg);

    #endregion
}
