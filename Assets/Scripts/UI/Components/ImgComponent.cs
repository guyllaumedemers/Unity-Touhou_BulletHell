using UnityEngine;
using UnityEngine.UI;

public class ImgComponent : IGraphicComponent
{
    public Image img { get; private set; }
    public RectTransform rect { get; private set; }
    public float anchpos { get; private set; }

    private void Awake()
    {
        rect = GameObject.FindGameObjectWithTag(Globals.slidingComponent).GetComponent<RectTransform>();
        img = GetComponent<Image>();

        if (!rect)
        {
            LogWarning($"The gameobject {gameObject.name} is not a UI element");
            return;
        }
        else if (!img)
        {
            LogWarning($"There is no image compoenent attach to the gameobject {gameObject.name}");
            return;
        }
        anchpos = rect.anchoredPosition.x;
    }

    public override void PlayGraphicAnimation()
    {
        // do something related to text behaviour
    }

    private void LogWarning(string msg) => Debug.LogWarning("[Img Component] " + msg);
}
