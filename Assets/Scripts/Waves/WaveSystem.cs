using System;
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
    public IDictionary<int, Queue<(string, int)>> waveDict;

    #region Wave System Functions

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
            Vector3[] cloneArr = waypoints.Clone() as Vector3[];
            if (spEnum != SpawningPosEnum.Both)
            {
                cloneArr = CheckEnumAndFlip(cloneArr, spEnum);
                StartInstanciationCoroutine<T>(name, move_behaviour, cloneArr[0], cloneArr, bulletType, maxUnitWave, interval);
                return;
            }
            StartInstanciationCoroutine<T>(name, move_behaviour, waypoints[0], waypoints, bulletType, maxUnitWave, interval);
            cloneArr = Utilities.FlipX(cloneArr, -1);
            StartInstanciationCoroutine<T>(name, move_behaviour, cloneArr[0], cloneArr, bulletType, maxUnitWave, interval);
        }
    }

    private void StartInstanciationCoroutine<T>(string name, IMoveable move_behaviour, Vector3 start_pos, Vector3[] waypoints, BulletTypeEnum bulletType,
        int maxUnitWave, float interval) where T : class
    {
        StartCoroutine(UnitManager.Instance.SequencialInit<T>(name, move_behaviour, start_pos, bulletType, waypoints, maxUnitWave, interval));
    }

    //TODO Need to make the bulletType relevant to the pattern the Unit plays : Is it known from a value in the serialize file?
    public IEnumerator InitializationMethod()
    {
        while (waveDict[level].Count > 0)
        {
            SpawningPosEnum spEnum = (SpawningPosEnum)UpdateDir(false);
            IMoveable move_behaviour = (curr_dir % variable_mod == 0) ? (IMoveable)new MoveableUnitCubicBezierB() : new MoveableUnitLinearBezierB();

            Launch<Unit>(waveDict[level].First().Item1, move_behaviour, WaypointSystem.GetWaypoints((curr_dir % variable_mod == 0), level, spEnum),
                BulletTypeEnum.Circle, spEnum, waveDict[level].First().Item2, Globals.initializationInterval);
            RemoveEntry();
            yield return new WaitForSeconds(Globals.waveInterval);
        }
        //TODO Reset Waypoints for new level
        //TODO Trigger event to go back to menu OR start a new wave
    }

    private void RemoveEntry() => waveDict[level].Dequeue();

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

    private Vector3[] CheckEnumAndFlip(Vector3[] myArr, SpawningPosEnum spEnum) => spEnum switch
    {
        SpawningPosEnum.Left => myArr,
        SpawningPosEnum.Right => Utilities.FlipX(myArr, -1),
        _ => throw new System.NotImplementedException()
    };

    #endregion

    #region Unity Functions

    //TODO PreIntilizationMethod will be called from the UI menu selection when the user select the stage
    public void PreIntilizationMethod(int levelSelect, int startingDir, int pivot, int var_mod)
    {
        level = levelSelect;
        curr_dir = startingDir;
        pivot_point = pivot;
        variable_mod = var_mod;
        //waveDict = Tool.XMLDeserialization_KVPTuple(Globals.XMLLevelinfo);
    }

    #endregion
}
