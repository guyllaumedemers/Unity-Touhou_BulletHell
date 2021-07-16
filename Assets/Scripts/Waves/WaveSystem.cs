using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaveSystem : SingletonMono<WaveSystem>
{
    private WaveSystem() { }

    public int stageSelection { get; private set; }
    public int curr_dir { get; private set; }
    public int pivot_point { get; private set; }
    public int variable_mod { get; private set; }
    public IDictionary<int, Queue<(string, int)>> waveDict = new Dictionary<int, Queue<(string, int)>>();

    /**********************ACTIONS**************************/

    private void Launch<T>(string name, IMoveable move_behaviour, Vector3[] waypoints, BulletTypeEnum bulletType, SpawningPosEnum spEnum, int maxUnitWave, float interval)
        where T : class
    {
        if (Utilities.CheckInterfaceType(move_behaviour, typeof(MoveableUnitLinearBezierB)))
        {
            if (spEnum != SpawningPosEnum.Both)
            {
                StartCoroutine(UnitManager.Instance.SequencialInit<T>(name, move_behaviour, waypoints[0], bulletType, waypoints, maxUnitWave, interval));
                return;
            }
            StartCoroutine(UnitManager.Instance.SequencialInit<T>(name, move_behaviour, waypoints[0], bulletType, Utilities.ParseArray(waypoints, 0, 3), maxUnitWave / 2, interval));
            StartCoroutine(UnitManager.Instance.SequencialInit<T>(name, move_behaviour, waypoints[3], bulletType, Utilities.ParseArray(waypoints, 3, 3), maxUnitWave / 2, interval));
        }
        else
        {
            Debug.Log("Instanciate units and pass spline waypoints as params");
        }
    }

    //INFO Update the direction in which the unit spawn
    //TODO NEED to figure out a way to parametrize so the bool doesnt fall in an infinite loop skipping an enum value like its currently doing
    private int UpdateDir(bool skip)
    {
        if (!skip)
        {
            curr_dir += curr_dir;
            if (curr_dir % pivot_point == 0) curr_dir = 1;
            return curr_dir;
        }
        return LateDirUpdate(curr_dir);
    }

    private int LateDirUpdate(int value)
    {
        curr_dir += value;
        if (curr_dir % pivot_point == 0) curr_dir = 1;
        return value;
    }

    /**********************FLOW****************************/

    //TODO PreIntilizationMethod will be called from the UI menu selection when the user select the stage
    public void PreIntilizationMethod(int stageselect, int startingDir, int pivot, int var_mod)
    {
        stageSelection = stageselect;
        curr_dir = startingDir;
        pivot_point = pivot;
        variable_mod = var_mod;
        waveDict = Tool.XMLDeserialization_KVPTuple(Globals.XMLLevelinfo);
    }

    //TODO Need to make the bulletType relevant to the pattern the Unit plays : Is it known from a value in the serialize file?
    public IEnumerator InitializationMethod()
    {
        while (waveDict[stageSelection].Count > 0)
        {
            SpawningPosEnum spEnum = (SpawningPosEnum)UpdateDir(false);
            IMoveable move_behaviour = (curr_dir % variable_mod == 0) ? (IMoveable)new MoveableUnitCubicBezierB() : new MoveableUnitLinearBezierB();

            Launch<Unit>(waveDict[stageSelection].First().Item1, move_behaviour, WaypointSystem.Instance.GetWaypoints((curr_dir % variable_mod == 0), stageSelection, spEnum),
                BulletTypeEnum.Circle, spEnum, waveDict[stageSelection].First().Item2, Globals.initializationInterval);
            RemoveEntry();
            yield return new WaitForSeconds(Globals.waveInterval);
        }
        //TODO Reset Waypoints for new level
        //TODO Trigger event to go back to menu OR start a new wave
    }

    /***************DATA STRUCTURE MANAGEMENT********************/

    private void RemoveEntry() => waveDict[stageSelection].Dequeue();
}
