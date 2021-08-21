using UnityEngine;

public class MenuButtonComponent : ButtonComponent
{
    RectTransform rect;

    protected override void Awake()
    {
        base.Awake();
        this.rect = GetComponent<RectTransform>();
        if (!rect)
        {
            LogWarning("This gameobject is not a UI element " + gameObject.name);
            return;
        }
        RegisterOperation(new BuzzingDecorator(this, mono, rect));
    }

    #region private functions
    private void LogWarning(string msg) => Debug.LogWarning("[Menu Button Component] : " + msg);
    #endregion
}
