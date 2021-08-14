using UnityEngine;
using UnityEngine.UI;

public class ImgComponent : IGraphicComponent
{
    public Image img { get; private set; }
    public RectTransform descriptionRect { get; private set; }
    public float anchpos { get; private set; }

    private void Awake()
    {
        img = GetComponent<Image>();
        descriptionRect = GameObject.FindGameObjectWithTag(Globals.slidingComponent).GetComponent<RectTransform>();

        if (!descriptionRect)
        {
            LogWarning("There is no RectTransform attach to the script");
            return;
        }
        anchpos = descriptionRect.anchoredPosition.x;
    }

    public override void PlayGraphicAnimation()
    {
        // do something related to text behaviour
    }

    private void LogWarning(string msg) => Debug.LogWarning("[Img Component] " + msg);
}
