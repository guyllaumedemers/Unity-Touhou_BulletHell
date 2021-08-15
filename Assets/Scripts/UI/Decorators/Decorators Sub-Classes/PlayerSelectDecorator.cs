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
        images = GetComponentsInChildren<Image>().Where(x => x.gameObject != gameObject).ToArray();
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
        foreach (var item in images) item.raycastTarget = false;
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
        //OnHooverEnter();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!images[0].enabled)
        {
            images[0].enabled = true;
        }
        //OnHooverExit();
    }

    #endregion

    #region private function

    private void OnHooverEnter()
    {
        FocusAnimationAlt(panelInstance.anchpos + 1000.0f);
    }

    private void OnHooverExit()
    {
        FocusAnimationAlt(panelInstance.anchpos);
    }

    private void FocusAnimation(float size)
    {
        StopCoroutine(typeof(CustomDotTween).GetMethods().Where(x => x.Name == "Focus").FirstOrDefault().Name);
        StartCoroutine(CustomDotTween.Focus(panelInstance.instance, size, Globals.buzzingTime));
    }

    private void FocusAnimationAlt(float endpos)
    {
        StopCoroutine(typeof(CustomDotTween).GetMethods().Where(x => x.Name == "FocusAlt").FirstOrDefault().Name);
        StartCoroutine(CustomDotTween.FocusAlt(panelInstance.instance, endpos, Globals.buzzingTime));
    }

    private void LogWarning(string msg) => Debug.LogWarning("[Player Select Decorator] : " + msg);

    #endregion
}
