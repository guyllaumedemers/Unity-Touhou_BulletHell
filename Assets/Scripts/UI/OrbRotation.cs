using UnityEngine;

public class OrbRotation : SingletonMono<OrbRotation>, IFlow
{
    private OrbRotation() { }
    private Vector3[] positions = new Vector3[]{
        // Collapse
        new Vector3((float)-0.5,0,0),
        new Vector3((float)-0.25,(float)0.4,0),
        new Vector3((float) 0.25,(float)0.4,0),
        new Vector3((float) 0.5,0,0),
        // Expand
        new Vector3((float)-0.75,0,0),
        new Vector3((float)-0.35,(float)0.5,0),
        new Vector3((float) 0.35,(float)0.5,0),
        new Vector3((float) 0.75,0,0),
    };

    #region Orb Functions

    public void ExpandAndCollapse(Vector3 centerPos, bool collapse)
    {
        Vector3[] result = Utilities.ParseArray(positions, collapse ? 0 : 4, 4);
        int count = -1;
        foreach (Transform orb in transform) orb.position = result[++count] + centerPos;
    }

    #endregion

    #region Unity Functions

    public void PreIntilizationMethod()
    {
        ExpandAndCollapse(Vector3.zero, true);
        foreach (Transform orb in transform) orb.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
    }

    public void InitializationMethod() { }

    public void UpdateMethod()
    {
        foreach (Transform orb in transform) orb.RotateAround(orb.position, transform.forward, Time.deltaTime * Globals.orbRotationSpeed);
    }

    #endregion
}
