using UnityEngine;
using UnityEngine.EventSystems;

public abstract class ButtonDecorator : IGraphicComponent
{
    protected IGraphicComponent buttonInstance;

    #region constructor
    public ButtonDecorator(IGraphicComponent component)
    {
        this.buttonInstance = component;
    }
    #endregion

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!buttonInstance.Equals(null)) buttonInstance.OnPointerClick(eventData);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!buttonInstance.Equals(null)) buttonInstance.OnPointerEnter(eventData);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!buttonInstance.Equals(null)) buttonInstance.OnPointerExit(eventData);
    }
}