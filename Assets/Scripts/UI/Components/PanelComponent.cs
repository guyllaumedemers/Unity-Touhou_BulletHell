using UnityEngine;

public class PanelComponent : IGraphicComponent
{
    public RectTransform instance { get; private set; }
    public float anchpos { get; private set; }

    private void Awake()
    {
        instance = GetComponent<RectTransform>();

        if (!instance)
        {
            LogWarning($"The gameobject {gameObject.name} is not a UI element");
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
