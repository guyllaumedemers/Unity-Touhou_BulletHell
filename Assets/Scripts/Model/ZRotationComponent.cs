using UnityEngine;
using UnityEngine.UI;

/*  ZRotation is not part of the decorator pattern as it could not implment the IGraphicComponent interface
 * 
 */

public class ZRotationComponent : MonoBehaviour
{
    private Image image;
    private float speed = 5.0f;

    protected void Awake()
    {
        image = GetComponentInChildren<Image>();
        if (!image)
        {
            LogWarning("There is no image to raycast on in this gameobject : " + gameObject.name);
            return;
        }
    }

    private void Update()
    {
        image.transform.Rotate(Vector3.forward * speed * Time.deltaTime, Space.Self);
    }
    private void LogWarning(string msg) => Debug.LogWarning("[ZRotation Component] : " + msg);
}
