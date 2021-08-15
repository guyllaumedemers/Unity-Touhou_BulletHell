using UnityEngine;
using UnityEngine.UI;

public class ImgComponent : IGraphicComponent
{
    public Image img { get; private set; }
    public float anchpos { get; private set; }

    private void Awake()
    {
        img = GetComponent<Image>();

        if (!img)
        {
            LogWarning($"There is no image compoenent attach to the gameobject {gameObject.name}");
            return;
        }
    }

    public override void PlayGraphicAnimation()
    {
        // do something related to text behaviour
    }

    private void LogWarning(string msg) => Debug.LogWarning("[Img Component] " + msg);
}
