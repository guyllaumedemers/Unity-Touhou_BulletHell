using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbRotation : SingletonMono<OrbRotation>, IFlow
{
    private OrbRotation() { }

    /**********************FLOW****************************/

    public void PreIntilizationMethod() { }

    public void InitializationMethod() { }

    public void UpdateMethod()
    {
        foreach (Transform orb in transform) orb.RotateAround(orb.position, transform.forward, Time.deltaTime * Globals.orbRotationSpeed);
    }
}
