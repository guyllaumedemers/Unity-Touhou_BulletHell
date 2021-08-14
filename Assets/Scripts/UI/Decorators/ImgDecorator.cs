using UnityEngine;
using UnityEngine.EventSystems;

public abstract class ImgDecorator : IGraphicComponent, IPointerEnterHandler, IPointerClickHandler
{
    public ImgComponent imgInstance { get; private set; }

    private void Awake()
    {
        imgInstance = gameObject.GetComponent<ImgComponent>();
        if (!imgInstance)
        {
            LogWarning($"There is no imgComponent script attach to this gameobject {gameObject.name}");
            return;
        }
    }

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

    #region  private functions

    private void LogWarning(string msg) => Debug.LogWarning("[Img Decorator] : " + msg);

    #endregion
}
