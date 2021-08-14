using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerSelectDecorator : ImgDecorator, IPointerExitHandler
{

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
        EntryPoint.Instance.TriggerNextScene();
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        imgInstance.img.color = Color.white;
    }

    public void OnPointerExit(PointerEventData eventData) => imgInstance.img.color = Color.grey;
}
