using System.Linq;
using UnityEngine;

public class BlurRenderer : MonoBehaviour
{
    private Camera blurcam;
    private Material blurMat;

    private void Awake()
    {
        if (!Camera.main)
        {
            LogWarning("There is no camera attach to the scene");
            return;
        }
        blurcam = Camera.main.transform.GetChild(0).GetComponent<Camera>();
        blurMat = Resources.LoadAll<Material>(Globals.shader).Where(x => x.name.Equals(Globals.blurMat)).FirstOrDefault();
    }

    private void Start()
    {
        if (blurcam.targetTexture != null)
        {
            blurcam.targetTexture.Release();
        }
        blurcam.targetTexture = new RenderTexture(Screen.width, Screen.height, 24, RenderTextureFormat.ARGB32, 1);
        blurMat.SetTexture("_RenTex", blurcam.targetTexture);
    }


    private void LogWarning(string msg) => Debug.LogWarning("[BlurRenderer] : " + msg);
}
