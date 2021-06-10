using System.Collections.Generic;
using Unity.Linq;
using UnityEngine;

public class WaypointSystem : SingletonMono<WaypointSystem>, IFlow
{
    private WaypointSystem() { }
    public Waypoint[] Waypoints { get; private set; }
    //// Access Waypoints positions from the dictionnary by selecting the active level
    //// Can be serialized and manage inside a JSON file later on
    private Dictionary<int, Vector2[]> positions = new Dictionary<int, Vector2[]>()
    {
        {0, new Vector2[]
            {
                new Vector2(0,0),
                new Vector2(0,1),
                new Vector2(1,0),
                new Vector2(1,1)
            }
        }
    };

    /**********************ACTIONS**************************/

    private Waypoint[] InitializeNewWaypointsForLevel(Dictionary<int, Vector2[]> positions, int level)
    {
        Waypoints = new Waypoint[positions[level].Length];
        Create(positions, level, Waypoints);
        return Waypoints;
    }

    private void Create(Dictionary<int, Vector2[]> positions, int level, Waypoint[] waypoints)
    {
        for (int i = 0; i < waypoints.Length; i++)
        {
            GameObject go = new GameObject(System.String.Format("{0} {1}", Globals.waypoint, i + 1));
            go.tag = Globals.waypoint;
            go.AddComponent<Waypoint>().SetPosition(positions[level][i]);
        }
    }

    //// Allows to clear current waypoints collection before switching levels
    private void ResetWaypoints() => GameObjectExtensions.Destroy(GameObject.FindGameObjectsWithTag(Globals.waypoint));

    /**********************FLOW****************************/

    public void PreIntilizationMethod() => InitializeNewWaypointsForLevel(positions, 0);

    public void InitializationMethod() { }

    public void UpdateMethod() { }
}
