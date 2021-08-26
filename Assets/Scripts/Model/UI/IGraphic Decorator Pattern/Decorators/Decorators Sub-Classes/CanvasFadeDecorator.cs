using UnityEngine;
using UnityEngine.EventSystems;

public class CanvasFadeDecorator : PanelDecorator
{
    private CanvasGroup alphagroup;
    private MonoBehaviour mono;
    private Coroutine routine = null;

    public CanvasFadeDecorator(IGraphicComponent decoratorInstance, MonoBehaviour mono, CanvasGroup alpha) : base(decoratorInstance)
    {
        if (!mono)
        {
            LogWarning("Did you break Unity?");
            return;
        }
        else if (!alpha)
        {
            LogWarning("There is no Canvas group component passed as arguments");
            return;
        }

        this.mono = mono;
        this.alphagroup = alpha;
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        mono.EnsureRoutineStop(ref routine);
        mono.CreateAnimationRoutine(Globals.curtainfade / 2, delegate (float progress)
        {
            float ease = EasingFunction.EaseInOutExpo(0, 1, progress);
            alphagroup.alpha = Mathf.Lerp(0, 1, ease);
        });
    }

    public override void OnPointerEnter(PointerEventData eventData) { }

    public override void OnPointerExit(PointerEventData eventData) { }

    #region private functions
    private void LogWarning(string msg) => Debug.LogWarning("[Canvas Decorator] : " + msg);
    #endregion
}
