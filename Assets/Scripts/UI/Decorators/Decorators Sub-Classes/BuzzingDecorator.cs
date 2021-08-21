using UnityEngine.EventSystems;

public class BuzzingDecorator : ButtonDecorator
{
    /*  Add buzzing animation
     * 
     */

    public BuzzingDecorator(IGraphicComponent component) : base(component) { }

    public override void OnPointerClick(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }
}
