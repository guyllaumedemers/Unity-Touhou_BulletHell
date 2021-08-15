using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerSelectDecorator : PanelDecorator, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private Image[] images;
    private TextMeshProUGUI[] texts;
    private Coroutine routine;

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
    }

    #region public functions

    public void OnPointerClick(PointerEventData eventData)
    {
        AudioManager.Instance.TriggerButtonClickSFX();
        this.EnsureRoutineStop(ref routine);
        this.CreateAnimationRoutine(Globals.sceneDelay, delegate (float progress) { }, () => { EntryPoint.Instance.TriggerNextScene(); });
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        AudioManager.Instance.TriggerMouseSFX();
        if (images[0].enabled)
        {
            images[0].enabled = false;
        }
        this.EnsureRoutineStop(ref routine);
        this.CreateAnimationRoutine(Globals.buzzingTime, delegate (float progress)
        {
            Vector2 start = panelInstance.instance.anchoredPosition;
            Vector2 end = new Vector2(panelInstance.instance.anchoredPosition.x, panelInstance.anchposY - panelInstance.instance.sizeDelta.y/2 + Globals.playerselectoffset);
            float ease = EasingFunction.EaseInOutSine(0, 1, progress);
            panelInstance.instance.anchoredPosition = Vector2.Lerp(start, end, ease);
        });
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!images[0].enabled)
        {
            images[0].enabled = true;
        }
        this.EnsureRoutineStop(ref routine);
        this.CreateAnimationRoutine(Globals.buzzingTime, delegate (float progress)
        {
            Vector2 start = panelInstance.instance.anchoredPosition;
            Vector2 end = new Vector2(panelInstance.instance.anchoredPosition.x, panelInstance.anchposY - panelInstance.instance.sizeDelta.y/2);
            float ease = EasingFunction.EaseInOutSine(0, 1, progress);
            panelInstance.instance.anchoredPosition = Vector2.Lerp(start, end, ease);
        });
    }

    #endregion

    #region private function

    private void LogWarning(string msg) => Debug.LogWarning("[Player Select Decorator] : " + msg);

    #endregion
}
