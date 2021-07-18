using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlurRenderer : MonoBehaviour
{
    Camera blurCamera;
    public Material blurMat;

    private void Awake()
    {
        blurCamera = gameObject.GetComponent<Camera>();
        if (blurCamera.targetTexture != null) blurCamera.targetTexture.Release();
        blurCamera.targetTexture = new RenderTexture(Screen.width, Screen.height, 24, RenderTextureFormat.ARGB32, 1);
        blurMat.SetTexture("_RenTex", blurCamera.targetTexture);
    }
}
