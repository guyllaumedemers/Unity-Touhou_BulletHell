using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectionConfirmBehaviour : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private Image img;

    private void Awake()
    {
        img = GetComponent<Image>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //PageController.Instance.TurnPageOff(PageTypeEnum.PlayerSelection, PageTypeEnum.Loading, true);
        //TODO wait for loading page to complete and trigger the next scene
    }

    public void OnPointerEnter(PointerEventData eventData) => img.color = Color.white;

    public void OnPointerExit(PointerEventData eventData) => img.color = Color.grey;
}
