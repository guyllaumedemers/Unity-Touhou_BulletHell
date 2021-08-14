using UnityEngine;
using UnityEngine.UI;

public class ImgComponent : IGraphicComponent
{
    public Image img { get; private set; }
    public RectTransform rect { get; private set; }
    public float anchpos { get; private set; }

    private void Awake()
    {
        img = GetComponent<Image>();
        rect = GameObject.FindGameObjectWithTag(Globals.slidingComponent).GetComponent<RectTransform>();

        if (!rect)
        {
            LogWarning("There is no RectTransform attach to the script");
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
