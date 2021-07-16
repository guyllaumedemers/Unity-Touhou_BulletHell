using System.Collections.Generic;
using Unity.Linq;
using UnityEngine;

public class WaypointSystem : SingletonMono<WaypointSystem>, IFlow
{
    private WaypointSystem() { }
    private GameObject waypointParent;
    public Waypoint[] Waypoints { get; private set; }
    public IDictionary<int, Vector3[]> positions = new Dictionary<int, Vector3[]>();

    /**********************ACTIONS**************************/

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

    //TODO Have to handle splines
    public Vector3[] GetLevelWPpos(int level, SpawningPosEnum sposEnum) => sposEnum switch
    {
        SpawningPosEnum.Left  => Utilities.ParseArray(positions[level], Globals.lsposParse, Globals.maxlengthParse),
        SpawningPosEnum.Right => Utilities.ParseArray(positions[level], Globals.rsposParse, Globals.maxlengthParse),
        SpawningPosEnum.Both  => Utilities.ParseArray(positions[level], Globals.bothsposParse, Globals.maxlengthParse * 2),
        //MAYBE have a special case for it // Would have to update the IMoveable assignation during unit creation to make it use cubic bezier curve
        _ => throw new System.ArgumentOutOfRangeException()
    };

    private void ResetWaypoints() => GameObjectExtensions.Destroy(GameObject.FindGameObjectsWithTag(Globals.waypoint));

    /**********************FLOW****************************/

    public void PreIntilizationMethod()
    {
        positions = Tool.XMLDeserialization_KVParray(Globals.XMLGameinfo);
        waypointParent = Utilities.InstanciateObjectParent(Globals.waypointParent, true);
        Waypoints = InitializeNewWaypointsForLevel(positions as Dictionary<int, Vector3[]>, default, waypointParent.transform);
    }

    public void InitializationMethod() { }

    public void UpdateMethod() { }
}
