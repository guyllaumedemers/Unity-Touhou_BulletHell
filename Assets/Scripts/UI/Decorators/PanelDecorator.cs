using UnityEngine;
using UnityEngine.EventSystems;

public abstract class PanelDecorator : IGraphicComponent
{
    /*  The decorator class is an abstract class that behaviours will implement 
     * 
     */

    protected IGraphicComponent panelInstance;

    #region constructor
    public PanelDecorator(IGraphicComponent component)
    {
        this.panelInstance = component;
    }
    #endregion

    #region interface
    public abstract void OnPointerClick(PointerEventData eventData);

    public abstract void OnPointerEnter(PointerEventData eventData);

    public abstract void OnPointerExit(PointerEventData eventData);
    #endregion
}
