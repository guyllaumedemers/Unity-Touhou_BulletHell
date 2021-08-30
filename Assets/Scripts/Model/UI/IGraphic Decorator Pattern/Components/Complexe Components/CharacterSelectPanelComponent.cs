using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterSelectPanelComponent : PanelComponent
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
    Image playerImg;
    CanvasGroup alphagroup;

    MonoBehaviour mono;

    protected override void Awake()
    {
        base.Awake();
        this.textComponents = GetComponentsInChildren<TextMeshProUGUI>();
        this.imgComponents = GetComponentsInChildren<Image>();
        this.myRect = GetComponent<RectTransform>();
        this.playerImg = transform.GetChild(0).GetComponentsInChildren<Image>().Last();
        this.alphagroup = FindObjectOfType<CanvasGroup>(true);

        this.mono = this;

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
        else if (!playerImg)
        {
            LogWarning("There is no image component in first child " + gameObject.name);
            return;
        }
        else if (textComponents.Length < 1)
        {
            LogWarning("There is no TextComponent on this gameobject " + gameObject.name);
            return;
        }
        else if (imgComponents.Length < 1)
        {
            LogWarning("There is no Image Component in the scene " + SceneManager.GetActiveScene().name);
            return;
        }
        else if (!alphagroup)
        {
            LogWarning("There is no Canvas Group Component in this scene " + SceneManager.GetActiveScene().name);
            return;
        }

        foreach (var item in textComponents) item.raycastTarget = false;
        foreach (var item in imgComponents.Where(x => x.gameObject.name != "Raycaster")) item.raycastTarget = false;

        RegisterOperation(new FocusPanelDecorator(this, mono, myRect));
        RegisterOperation(new BlinkingImgDecorator(this, mono, playerImg));
        RegisterOperation(new SelectableImgPanelDecorator(this, playerImg));
        RegisterOperation(new CanvasFadeDecorator(this, mono, alphagroup));
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
        if (raycaster.raycastTarget)
        {
            imgComponents.Where(x => x.gameObject.name.Equals("Raycaster")).FirstOrDefault().raycastTarget = false;
            SceneController.Instance.TriggerNextScene(Globals.longFadingTime);
        }
    }

    private void LogWarning(string msg) => Debug.LogWarning("[PlayerSelect Component] : " + msg);
}
