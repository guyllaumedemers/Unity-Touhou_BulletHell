using TMPro;
using UnityEngine;

public class MenuButtonComponent : ButtonComponent
{
    MonoBehaviour mono;
    RectTransform rect;
    TextMeshProUGUI text;

    private void Awake()
    {
        this.mono = this;
        this.rect = GetComponent<RectTransform>();
        this.text = GetComponentInChildren<TextMeshProUGUI>();

        if (!mono)
        {
            LogWarning("Wait what how can it be attach to a gameobject if not monobehaviour, did you break Unity?");
            return;
        }
        else if (!rect)
        {
            LogWarning("This gameobject is not a UI element " + gameObject.name);
            return;
        }
        else if (!text)
        {
            LogWarning("There is no text component attach to this gameobject " + gameObject.name);
            return;
        }

        RegisterOperation(new BlinkingTextDecorator(this, mono, text));
        RegisterOperation(new BuzzingDecorator(this, mono, rect));
    }

    #region private functions
    private void LogWarning(string msg) => Debug.LogWarning("[Focus panel Decorator] : " + msg);
    #endregion
}
