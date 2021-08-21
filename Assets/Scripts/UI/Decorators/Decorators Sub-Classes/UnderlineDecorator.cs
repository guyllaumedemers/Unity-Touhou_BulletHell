using UnityEngine.EventSystems;

public class UnderlineDecorator : ButtonDecorator
{
    /*  Add underline
     * 
     */

    public UnderlineDecorator(IGraphicComponent component) : base(component) { }

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
