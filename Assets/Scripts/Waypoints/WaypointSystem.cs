using System.Collections.Generic;
using Unity.Linq;
using UnityEngine;

public class WaypointSystem : SingletonMono<WaypointSystem>, IFlow
{
    private WaypointSystem() { }
    private GameObject waypointParent;
    public Waypoint[] Waypoints { get; private set; }
    public IDictionary<int, Vector3[]> positions;

    #region Waypoint System Functions

    private Waypoint[] InitializeNewWaypointsForLevel(Dictionary<int, Vector3[]> positions, int level, Transform parent)
    {
        return Create(positions, level, parent);
    }

    private Waypoint[] Create(Dictionary<int, Vector3[]> positions, int level, Transform parent)
    {
        List<Waypoint> points = new List<Waypoint>();
        for (int i = 0; i < positions[level].Length; i++)
        {
            GameObject go = Utilities.InstanciateObjectParent(System.String.Format("{0} {1}", Globals.waypoint, i + 1), true);
            go.tag = Globals.waypoint;
            go.transform.SetParent(parent);
            go.AddComponent<Waypoint>().SetPosition(positions[level][i]);
            points.Add(go.GetComponent<Waypoint>());
        }
        return points.ToArray();
    }

    private Vector3[] GetLevelWPpos(int level, SpawningPosEnum spEnum) => spEnum switch
    {
        SpawningPosEnum.Left => Utilities.ParseArray(positions[level], Globals.lsposParse, Globals.maxlengthParse),
        SpawningPosEnum.Right => Utilities.ParseArray(positions[level], Globals.rsposParse, Globals.maxlengthParse),
        SpawningPosEnum.Both => Utilities.ParseArray(positions[level], Globals.bothsposParse, Globals.maxlengthParse * 2),
        SpawningPosEnum.None => Utilities.ParseArray(positions[level], Globals.splinesposParse, Globals.maxlengthsplineParse),
        _ => throw new System.ArgumentOutOfRangeException()
    };

    //INFO SpawningPosEnum is not taken into consideration on the WaveSystem side as it isnt part of the update function for setting the SpawningEnum used for managing sides
    //WHY? Because I want to be able to use spline while being able to set units from both side of the screens
    //I will only have to flip the starting pos of the unit using the spline depending on the side the Enum so the unit start at the proper position outside the screen
    public Vector3[] GetWaypoints(bool moveInterfaceSelectIsCubic, int level, SpawningPosEnum spEnum)
    {
        if (moveInterfaceSelectIsCubic) return GetLevelWPpos(level, SpawningPosEnum.None);
        else return GetLevelWPpos(level, spEnum);
    }

    private void ResetWaypoints() => GameObjectExtensions.Destroy(GameObject.FindGameObjectsWithTag(Globals.waypoint));

    #endregion

    #region Unity Functions

    public void PreIntilizationMethod()
    {
        positions = Tool.XMLDeserialization_KVParray(Globals.XMLGameinfo);
        waypointParent = Utilities.InstanciateObjectParent(Globals.waypointParent, true);
        Waypoints = InitializeNewWaypointsForLevel(positions as Dictionary<int, Vector3[]>, default, waypointParent.transform);
    }

    public void InitializationMethod() { }

    public void UpdateMethod() { }

    #endregion
}
