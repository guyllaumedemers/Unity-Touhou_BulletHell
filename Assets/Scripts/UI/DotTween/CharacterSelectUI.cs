using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharacterSelectUI : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private Image img;

    private void Awake()
    {
        img = GetComponent<Image>();
    }

    public void OnPointerClick(PointerEventData eventData) => EntryPoint.Instance.TriggerNextScene();

    public void OnPointerEnter(PointerEventData eventData) => img.color = Color.white;

    public void OnPointerExit(PointerEventData eventData) => img.color = Color.grey;
}
