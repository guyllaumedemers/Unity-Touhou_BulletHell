using UnityEngine;
using UnityEngine.EventSystems;

public abstract class ButtonDecorator : IGraphicComponent, IPointerEnterHandler, IPointerClickHandler
{
    public ButtonComponent buttonInstance { get; private set; }

    private void Awake()
    {
        buttonInstance = GetComponent<ButtonComponent>();
        if (!buttonInstance)
        {
            LogWarning($"There is no buttonComponent script attach to this gameobject {gameObject.name}");
            return;
        }
    }

    #region public functions

    public virtual void OnPointerEnter(PointerEventData eventData) => buttonInstance.OnPointerEnterSFX();
    public virtual void OnPointerClick(PointerEventData eventData) => buttonInstance.OnPointerClickSFX();

    public override void PlayGraphicAnimation()
    {
        buttonInstance.PlayGraphicAnimation();
        // add decorator behaviour if any
    }
    #endregion

    #region  private functions

    private void LogWarning(string msg) => Debug.LogWarning("[Button Decorator] : " + msg);

    #endregion
}
