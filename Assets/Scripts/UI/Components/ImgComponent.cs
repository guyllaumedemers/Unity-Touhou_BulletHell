using UnityEngine.UI;

public class ImgComponent : IGraphicComponent
{
    public Image img { get; private set; }

    private void Awake() => img = GetComponent<Image>();

    public override void PlayGraphicAnimation()
    {
        // do something related to text behaviour
    }
}
