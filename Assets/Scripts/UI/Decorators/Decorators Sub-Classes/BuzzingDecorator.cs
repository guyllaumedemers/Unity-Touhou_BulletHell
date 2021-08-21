using System.Linq;
using UnityEngine.EventSystems;

public class BuzzingDecorator : ButtonDecorator
{
    /*  Add buzzing animation
     * 
     */

    public BuzzingDecorator(IGraphicComponent component) : base(component) { }
}
