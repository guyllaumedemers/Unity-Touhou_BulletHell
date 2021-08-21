using TMPro;
using UnityEngine;

public class MenuButtonComponent : ButtonComponent
{
    MonoBehaviour mono;
    TextMeshProUGUI text;

    private void Awake()
    {
        this.mono = this;
        this.text = GetComponentInChildren<TextMeshProUGUI>();

        if (!mono)
        {
            LogWarning("Wait what how can it be attach to a gameobject if not monobehaviour, did you break Unity?");
            return;
        }
        else if (!text)
        {
            LogWarning("There is no text component attach to this gameobject " + gameObject.name);
            return;
        }

        RegisterOperation(new BlinkingTextDecorator(this, mono, text));
    }

    #region private functions
    private void LogWarning(string msg) => Debug.LogWarning("[Focus panel Decorator] : " + msg);
    #endregion
}
