using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PanelComponent : IGraphicComponent
{
    public RectTransform[] rects { get; private set; }
    public RectTransform instance { get; private set; }

    private void Awake()
    {
        rects = GetComponentsInChildren<Button>().Select(x => x.GetComponent<RectTransform>()).ToArray();
        instance = GetComponent<RectTransform>();

        if (rects.Length <= 0)
        {
            LogWarning("There is no button child in this component");
            return;
        }
        else if (!instance)
        {
            LogWarning("The game object on which this script is attach is not a UI component");
            return;
        }
    }

    public override void PlayGraphicAnimation()
    {
        // do PanelComponent behaviours
    }

    #region private functions

    private void LogWarning(string msg) => Debug.LogWarning("[Panel Component] : " + msg);

    #endregion
}
