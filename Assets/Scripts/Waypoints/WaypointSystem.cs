using System.Collections.Generic;
using Unity.Linq;
using UnityEngine;

public class WaypointSystem : SingletonMono<WaypointSystem>, IFlow
{
    private WaypointSystem() { }
    public Waypoint[] Waypoints { get; private set; }
    private GameObject waypointParent;
    //// Access Waypoints positions from the dictionnary by selecting the active level
    //// Can be serialized and manage inside a JSON file later on
    private Dictionary<int, Vector3[]> positions = new Dictionary<int, Vector3[]>()
    {
        {0, new Vector3[]
            {
                new Vector3(-2,5), new Vector3(0,0), new Vector3(-4,0),     // left
                new Vector3(2,5), new Vector3(0,0), new Vector3(5,0),       // right
                new Vector3(0,5)                                            // middle
            }
        }
    };

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

    // quick solution that doesnt realy handle a boss waypoints behaviour
    public Vector3[] GetLevelWPpos(int level, SpawningPosEnum sposEnum) => sposEnum switch
    {
        SpawningPosEnum.Right => Utilities.ParseArray(positions[level], Globals.rsPos_parse, Globals.max_parse),
        SpawningPosEnum.Left => Utilities.ParseArray(positions[level], Globals.lsPos_parse, Globals.max_parse),
        SpawningPosEnum.None => positions[level],
        _ => throw new System.ArgumentOutOfRangeException()
    };

    //// Allows to clear current waypoints collection before switching levels
    private void ResetWaypoints() => GameObjectExtensions.Destroy(GameObject.FindGameObjectsWithTag(Globals.waypoint));

    /**********************FLOW****************************/

    public void PreIntilizationMethod()
    {
        waypointParent = Utilities.InstanciateObjectParent(Globals.waypointParent, true);
        Waypoints = InitializeNewWaypointsForLevel(positions, 0, waypointParent.transform);         // level value will be handle by the level manager eventually
    }

    public void InitializationMethod() { }

    public void UpdateMethod() { }
}
