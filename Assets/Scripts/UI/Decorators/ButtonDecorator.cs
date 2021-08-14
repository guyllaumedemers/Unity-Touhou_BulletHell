using UnityEngine;
using UnityEngine.EventSystems;

public abstract class ButtonDecorator : IGraphicComponent, IPointerEnterHandler, IPointerClickHandler
{
    public ButtonComponent buttonInstance { get; private set; }

    private void Awake() => buttonInstance = gameObject.GetComponent<ButtonComponent>();

    #region public functions

    #region events
    public virtual void OnPointerClick(PointerEventData eventData) => buttonInstance.OnPointerClickSFX();
    public virtual void OnPointerEnter(PointerEventData eventData) => buttonInstance.OnPointerEnterSFX();
    #endregion

    public override void PlayGraphicAnimation() => buttonInstance.PlayGraphicAnimation();
    #endregion
}