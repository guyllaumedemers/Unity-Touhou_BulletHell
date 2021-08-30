using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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
    protected Image raycaster;

    protected virtual void Awake()
    {
        raycaster = GetComponentsInChildren<Image>().Where(x => x.gameObject.name.Equals("Raycaster")).FirstOrDefault();
        if (!raycaster)
        {
            LogWarning("There is no raycaster image on this gameobject : " + gameObject.name);
            return;
        }
    }

    #region interface
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (raycaster.raycastTarget)
        {
            AudioManager.Instance.TriggerMouseSFX();
            foreach (var item in panelmodifiers) item.OnPointerEnter(eventData);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (raycaster.raycastTarget)
        {
            foreach (var item in panelmodifiers) item.OnPointerExit(eventData);
        }
    }

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        if (raycaster.raycastTarget)
        {
            AudioManager.Instance.TriggerButtonClickSFX();
            foreach (var item in panelmodifiers) item.OnPointerClick(eventData);
        }
    }
    #endregion

    #region public functions
    public void RegisterOperation(PanelDecorator operation) => panelmodifiers.Add(operation);
    #endregion

    private void LogWarning(string msg) => Debug.LogWarning("[Panel Component] : " + msg);
}
