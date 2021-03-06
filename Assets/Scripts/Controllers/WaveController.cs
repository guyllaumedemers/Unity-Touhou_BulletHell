using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaveController : SingletonMono<WaveController>
{
    private Queue<Tuple<string, int>> waveQueue;
    private int curr_dir;
    private int pivot_point;
    private int variable_mod;

    //TODO Need to make the bulletType relevant to the pattern the Unit plays : Is it known from a value in the serialize file?
    public IEnumerator StartWave(int level, int dir, int pivot, int var_mod)
    {
        InitializeLevel(level, dir, pivot, var_mod);
        while (waveQueue.Count > 0)
        {
            DirectionEnum spEnum = (DirectionEnum)UpdateDir(false);
            IMoveable move_behaviour = (curr_dir % variable_mod == 0) ? (IMoveable)new MoveableUnitCubicBezierB() : new MoveableUnitLinearBezierB();

            Launch(waveQueue.First().Item1, move_behaviour, WaypointController.GetWaypoints((curr_dir % variable_mod == 0), level, spEnum),
                BulletTypeEnum.Circle, spEnum, waveQueue.First().Item2, Globals.initializationInterval);
            RemoveEntry();
            yield return new WaitForSeconds(Globals.waveInterval);
        }
        //TODO Trigger event to go back to menu OR start a new wave
    }

    #region private functions
    //TODO PreIntilizationMethod will be called from the UI menu selection when the user select the stage
    private void InitializeLevel(int level, int dir, int pivot, int var_mod)
    {
        curr_dir = dir;
        pivot_point = pivot;
        variable_mod = var_mod;
        waveQueue = WrappingTool.EncapsulateInQueue(DatabaseHandler.RetrieveTableEntries<Tuple<string, int>>(Globals.waveTable, $"WHERE Id = {level}"));
    }
    private void Launch(string name, IMoveable move_behaviour, Vector3[] waypoints, BulletTypeEnum bulletType, DirectionEnum spEnum, int maxUnitWave, float interval)
    {
        if (Utilities.CheckInterfaceType(move_behaviour, typeof(MoveableUnitLinearBezierB)))
        {
            if (spEnum != DirectionEnum.Both)
            {
                StartInstanciationCoroutine(name, move_behaviour, waypoints[0], waypoints, bulletType, maxUnitWave, interval);
                return;
            }
            StartInstanciationCoroutine(name, move_behaviour, waypoints[0], Utilities.ParseArray(waypoints, 0, 3), bulletType, maxUnitWave, interval);
            StartInstanciationCoroutine(name, move_behaviour, waypoints[3], Utilities.ParseArray(waypoints, 3, 3), bulletType, maxUnitWave, interval);
        }
        else
        {
            Vector3[] cloneArr = waypoints.Clone() as Vector3[];
            if (spEnum != DirectionEnum.Both)
            {
                cloneArr = CheckEnumAndFlip(cloneArr, spEnum);
                StartInstanciationCoroutine(name, move_behaviour, cloneArr[0], cloneArr, bulletType, maxUnitWave, interval);
                return;
            }
            StartInstanciationCoroutine(name, move_behaviour, waypoints[0], waypoints, bulletType, maxUnitWave, interval);
            cloneArr = Utilities.FlipX(cloneArr, -1);
            StartInstanciationCoroutine(name, move_behaviour, cloneArr[0], cloneArr, bulletType, maxUnitWave, interval);
        }
    }
    private void StartInstanciationCoroutine(string name, IMoveable move_behaviour, Vector3 start_pos, Vector3[] waypoints, BulletTypeEnum bulletType,
        int maxUnitWave, float interval)
    {
        StartCoroutine(UnitManager.Instance.SequencialInit(name, move_behaviour, start_pos, bulletType, waypoints, maxUnitWave, interval));
    }
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
    private Vector3[] CheckEnumAndFlip(Vector3[] myArr, DirectionEnum spEnum)
    {
        return spEnum switch
        {
            DirectionEnum.Left => myArr,
            DirectionEnum.Right => Utilities.FlipX(myArr, -1),
            _ => throw new System.NotImplementedException()
        };
    }
    private void RemoveEntry() => waveQueue.Dequeue();
    #endregion
}
