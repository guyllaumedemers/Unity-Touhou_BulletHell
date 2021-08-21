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

    public abstract void OnPointerClick(PointerEventData eventData);

    public abstract void OnPointerEnter(PointerEventData eventData);

    public abstract void OnPointerExit(PointerEventData eventData);
}