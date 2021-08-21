using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSelectComponent : PanelComponent
{
    /*  PlayerSelect needs to disable all Text Components AND Image Components in the UI gameobject in order to avoid raycast blocking
     *  that was not set false on the gameobject
     *  
     *  IMPORTANT : Raycaster object must be name Raycaster in order to not see his raycastTarget option set to false
     * 
     */

    TextMeshProUGUI[] textComponents;
    Image[] imgComponents;
    RectTransform myRect;

    MonoBehaviour mono;

    private void Awake()
    {
        textComponents = GetComponentsInChildren<TextMeshProUGUI>();
        imgComponents = GetComponentsInChildren<Image>();
        myRect = GetComponent<RectTransform>();

        mono = this;

        if (!mono)
        {
            LogWarning("Wait what how can it be attach to a gameobject if not monobehaviour, did you break Unity?");
            return;
        }
        else if (!myRect)
        {
            LogWarning("This is not a UI component " + gameObject.name);
            return;
        }
        else if (textComponents.Length < 1)
        {
            LogWarning("There is no TextComponent on this gameobject " + gameObject.name);
            return;
        }
        else if (imgComponents.Length < 1)
        {
            LogWarning("There is no Image Component on this gameobject " + gameObject.name);
            return;
        }

        foreach (var item in textComponents) item.raycastTarget = false;
        foreach (var item in imgComponents.Where(x => x.gameObject.name != "Raycaster")) item.raycastTarget = false;

        RegisterOperation(new FocusPanelDecorator(this, mono, myRect));
        RegisterOperation(new SelectableImgPanelDecorator(this));
    }

    private void LogWarning(string msg) => Debug.LogWarning("[PlayerSelect Component] : " + msg);
}