using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PanelComponent : MonoBehaviour, IGraphicComponent
{
    /*  The component base class implement the base interface for the decorator pattern
     *  
     *  In the component class is defined a set of behaviours that compose the class. These behaviours extend the component class
     *  which alter its base behaviour
     *  
     *  This is the script that will be attach to the gameobject
     *  
     *  It also imply that a new script needs to be created if we want a different behaviour for the gameobject. In our case the base class PanelComponent
     *  doesnt implement any behaviours.
     *  
     *  The derived class will be in charge of setting his own set of behaviours
     * 
     */

    private List<PanelDecorator> panelmodifiers = new List<PanelDecorator>();

    #region interface
    public void OnPointerEnter(PointerEventData eventData)
    {
        foreach (var item in panelmodifiers) item.OnPointerEnter(eventData);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        foreach (var item in panelmodifiers) item.OnPointerExit(eventData);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        foreach (var item in panelmodifiers) item.OnPointerClick(eventData);
    }
    #endregion

    #region public functions
    public void RegisterOperation(PanelDecorator operation) => panelmodifiers.Add(operation);
    #endregion
}
