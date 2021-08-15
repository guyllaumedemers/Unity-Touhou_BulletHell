using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/*  Might have to refactor this later as it doesnt quite feels like a decorator sub-class
 *  
 *  Have issues using the blinkingImgUI Decorator since it cannot retrieve the raycast information associated with his image component
 *  Might have to remove the scripts and handle the behaviour here which isnt optiomal
 */

public class PlayerSelectDecorator : PanelDecorator, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private Image[] images;
    private TextMeshProUGUI[] texts;
    private Coroutine routine;

    /*  Images array index for the character display is equal to 1 - reordering the component hierarchy will result in value changes
     */

    protected override void Awake()
    {
        base.Awake();
        images = GetComponentsInChildren<Image>();
        texts = GetComponentsInChildren<TextMeshProUGUI>();
        if (images.Length < 1)
        {
            LogWarning($"There is no child image attach to this gameobject {gameObject.name}");
            return;
        }
        else if (texts.Length < 1)
        {
            LogWarning($"There is no child text component attach to this gameobject {gameObject.name}");
            return;
        }
        foreach (var item in images.Where(x => x.gameObject.name != "Raycaster")) item.raycastTarget = false;
        foreach (var item in texts) item.raycastTarget = false;

        images[1].color = CustomDotTween.UpdateColor(Color.grey);
    }

    #region public functions

    public void OnPointerClick(PointerEventData eventData)
    {
        AudioManager.Instance.TriggerButtonClickSFX();
        StopCoroutine(typeof(CustomDotTween).GetMethods().Where(x => x.Name == "BlinkingImgUI").FirstOrDefault().Name);     // should be done via the blinkingDecorator
        StartCoroutine(CustomDotTween.BlinkingImgUI(images[1], Globals.blinkingTime, 5));
        this.EnsureRoutineStop(ref routine);
        this.CreateAnimationRoutine(Globals.pageAnimationWaitTime, delegate (float progress) { }, () => { EntryPoint.Instance.TriggerNextScene(); });
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        AudioManager.Instance.TriggerMouseSFX();
        images[1].color = CustomDotTween.UpdateColor(Color.white);

        this.EnsureRoutineStop(ref routine);
        this.CreateAnimationRoutine(Globals.buzzingTime, delegate (float progress)
        {
            Vector2 start = panelInstance.instance.anchoredPosition;
            Vector2 end = new Vector2(panelInstance.instance.anchoredPosition.x, panelInstance.anchposY - panelInstance.instance.sizeDelta.y / 2 + Globals.playerselectoffset);
            float ease = EasingFunction.EaseInOutSine(0, 1, progress);
            panelInstance.instance.anchoredPosition = Vector2.Lerp(start, end, ease);
        });
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        images[1].color = CustomDotTween.UpdateColor(Color.grey);

        this.EnsureRoutineStop(ref routine);
        this.CreateAnimationRoutine(Globals.buzzingTime, delegate (float progress)
        {
            Vector2 start = panelInstance.instance.anchoredPosition;
            Vector2 end = new Vector2(panelInstance.instance.anchoredPosition.x, panelInstance.anchposY - panelInstance.instance.sizeDelta.y / 2);
            float ease = EasingFunction.EaseInOutSine(0, 1, progress);
            panelInstance.instance.anchoredPosition = Vector2.Lerp(start, end, ease);
        });
    }

    #endregion

    #region private function

    private void LogWarning(string msg) => Debug.LogWarning("[Player Select Decorator] : " + msg);

    #endregion
}
