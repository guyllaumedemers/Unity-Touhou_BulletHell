using UnityEngine;
using UnityEngine.EventSystems;

public class ImgDecorator : IGraphicComponent, IPointerEnterHandler, IPointerClickHandler
{
    public ImgComponent imgInstance { get; private set; }

    private void Awake() => imgInstance = gameObject.GetComponent<ImgComponent>();

    #region public functions

    #region events

    public void OnPointerEnterSFX() => AudioManager.Instance.TriggerMouseSFX();
    public void OnPointerClickSFX() => AudioManager.Instance.TriggerButtonClickSFX();

    #endregion

    public override void PlayGraphicAnimation()
    {
        // do img behaviour
    }

    public virtual void OnPointerClick(PointerEventData eventData) => OnPointerClickSFX();

    public virtual void OnPointerEnter(PointerEventData eventData) => OnPointerEnterSFX();

    #endregion
}
