using UnityEngine;

public class Rotation : MonoBehaviour
{
    Transform myT;
    private void Awake() => myT = gameObject.GetComponent<Transform>();

    private void Update()
    {
        myT.RotateAround(myT.position, transform.forward, Time.deltaTime * Globals.spinningUnitRotationSpeed);
    }
}
