using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonComponent : MonoBehaviour, IGraphicComponent
{
    public List<ButtonDecorator> buttonModifiers = new List<ButtonDecorator>();

    #region interface
    public void OnPointerClick(PointerEventData eventData)
    {
        foreach (var item in buttonModifiers) item.OnPointerClick(eventData);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        foreach (var item in buttonModifiers) item.OnPointerEnter(eventData);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        foreach (var item in buttonModifiers) item.OnPointerExit(eventData);
    }
    #endregion

    #region public functions
    public void RegisterOperation(ButtonDecorator operation) => buttonModifiers.Add(operation);
    #endregion
}
