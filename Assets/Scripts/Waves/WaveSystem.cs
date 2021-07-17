using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaveSystem : SingletonMono<WaveSystem>
{
    private WaveSystem() { }

    public int level { get; private set; }
    public int curr_dir { get; private set; }
    public int pivot_point { get; private set; }
    public int variable_mod { get; private set; }
    public IDictionary<int, Queue<(string, int)>> waveDict = new Dictionary<int, Queue<(string, int)>>();

    /**********************ACTIONS**************************/

    //HINT Launch trigger the start of a wave and manage the behaviour for unit creation, handling assignation of waypoints array for left / right / both side
    //AND  also handle the assignation of positions for splines for all sides 
    private void Launch<T>(string name, IMoveable move_behaviour, Vector3[] waypoints, BulletTypeEnum bulletType, SpawningPosEnum spEnum, int maxUnitWave, float interval)
        where T : class
    {
        if (Utilities.CheckInterfaceType(move_behaviour, typeof(MoveableUnitLinearBezierB)))
        {
            if (spEnum != SpawningPosEnum.Both)
            {
                StartInstanciationCoroutine<T>(name, move_behaviour, waypoints[0], waypoints, bulletType, maxUnitWave, interval);
                return;
            }
            StartInstanciationCoroutine<T>(name, move_behaviour, waypoints[0], Utilities.ParseArray(waypoints, 0, 3), bulletType, maxUnitWave, interval);
            StartInstanciationCoroutine<T>(name, move_behaviour, waypoints[3], Utilities.ParseArray(waypoints, 3, 3), bulletType, maxUnitWave, interval);
        }
        else
        {
            if (spEnum != SpawningPosEnum.Both)
            {
                StartInstanciationCoroutine<T>(name, move_behaviour, FlipArray(waypoints, spEnum)[0], FlipArray(waypoints, spEnum), bulletType, maxUnitWave, interval);
                return;
            }
            StartInstanciationCoroutine<T>(name, move_behaviour, waypoints[0], waypoints, bulletType, maxUnitWave, interval);
            StartInstanciationCoroutine<T>(name, move_behaviour, Utilities.ReverseArray(waypoints)[0], Utilities.ReverseArray(waypoints), bulletType, maxUnitWave, interval);
        }
    }

    private void StartInstanciationCoroutine<T>(string name, IMoveable move_behaviour, Vector3 start_pos, Vector3[] waypoints, BulletTypeEnum bulletType, int maxUnitWave, float interval)
        where T : class
    {
        StartCoroutine(UnitManager.Instance.SequencialInit<T>(name, move_behaviour, start_pos, bulletType, waypoints, maxUnitWave, interval));
    }

    private Vector3[] FlipArray(Vector3[] myArr, SpawningPosEnum spEnum) => spEnum switch
    {
        SpawningPosEnum.Left => myArr,
        SpawningPosEnum.Right => Utilities.ReverseArray(myArr),
        _ => throw new System.NotImplementedException()
    };

    //TODO UpdateDir is currently creating a loop where a specific value is never visited
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
    public void PreIntilizationMethod(int levelSelect, int startingDir, int pivot, int var_mod)
    {
        level = levelSelect;
        curr_dir = startingDir;
        pivot_point = pivot;
        variable_mod = var_mod;
        waveDict = Tool.XMLDeserialization_KVPTuple(Globals.XMLLevelinfo);
    }

    //TODO Need to make the bulletType relevant to the pattern the Unit plays : Is it known from a value in the serialize file?
    public IEnumerator InitializationMethod()
    {
        while (waveDict[level].Count > 0)
        {
            SpawningPosEnum spEnum = (SpawningPosEnum)UpdateDir(false);
            IMoveable move_behaviour = (curr_dir % variable_mod == 0) ? (IMoveable)new MoveableUnitCubicBezierB() : new MoveableUnitLinearBezierB();

            Launch<Unit>(waveDict[level].First().Item1, move_behaviour, WaypointSystem.Instance.GetWaypoints((curr_dir % variable_mod == 0), level, spEnum),
                BulletTypeEnum.Circle, spEnum, waveDict[level].First().Item2, Globals.initializationInterval);
            RemoveEntry();
            yield return new WaitForSeconds(Globals.waveInterval);
        }
        //TODO Reset Waypoints for new level
        //TODO Trigger event to go back to menu OR start a new wave
    }

    /***************DATA STRUCTURE MANAGEMENT********************/

    private void RemoveEntry() => waveDict[level].Dequeue();
}
