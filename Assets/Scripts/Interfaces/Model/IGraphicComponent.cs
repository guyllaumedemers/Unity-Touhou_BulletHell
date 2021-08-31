using UnityEngine;
using UnityEngine.EventSystems;

public interface IGraphicComponent : IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    /*  There is no function declaration inside this interface because they are declared inside the inherited interfaces
     *  
     *  A typical Decorator pattern would declare his own functions in his interface which would be later implemented in the derived classes
     * 
     */
}
