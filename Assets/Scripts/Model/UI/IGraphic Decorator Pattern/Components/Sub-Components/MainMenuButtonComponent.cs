using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenuButtonComponent : ButtonComponent
{
    private RectTransform rect;
    private Button[] buttons;

    protected override void Awake()
    {
        base.Awake();
        this.rect = GetComponent<RectTransform>();
        this.buttons = FindObjectsOfType<Button>().Where(x => x.GetComponent<MainMenuButtonComponent>()).ToArray();

        if (!rect)
        {
            LogWarning("This gameobject is not a UI element " + gameObject.name);
            return;
        }
        else if (buttons.Length < 1)
        {
            LogWarning("There is no buttons with the Main Button Component script attach");
            return;
        }
        RegisterOperation(new BuzzingDecorator(this, mono, rect));
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
        if (button.interactable)
        {
            foreach (var item in buttons) item.interactable = false;
        }
    }

    #region private functions
    private void LogWarning(string msg) => Debug.LogWarning("[Menu Button Component] : " + msg);
    #endregion
}
