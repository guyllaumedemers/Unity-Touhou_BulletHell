using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitManager
{
    private static UnitManager instance;
    private UnitManager() { }
    public static UnitManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new UnitManager();
            }
            return instance;
        }
    }

    public Dictionary<string, HashSet<Unit>> UnitsDict { get; private set; }
    public GameObject[] Units { get; private set; }

    private Queue<Unit> UnitPool;

    #region public functions
    public IEnumerator SequencialInit(string name, IMoveable move_behaviour, Vector3 pos, BulletTypeEnum bulletType, Vector3[] waypoints, int maxUnitWave, float interval)
    {
        int curr_count = -1;
        while (++curr_count < maxUnitWave)
        {
            Create(name, pos, move_behaviour, waypoints, bulletType);
            yield return new WaitForSeconds(interval);
        }
    }
    public Unit Create(string type, Vector2 pos, IMoveable move_behaviour, Vector3[] waypoints, BulletTypeEnum bulletT)
    {
        Unit instance = Utilities.InstanciateType<Unit>(ResourcesLoader.GetPrefab(Units, type), null, pos);
        Add(type, instance);
        return instance.PreInitializeUnit(type, move_behaviour, waypoints, bulletT);
    }
    #endregion


    #region private functions
    private void UpdateUnits(Dictionary<string, HashSet<Unit>> dict, Queue<Unit> pool)
    {
        foreach (var unit in dict.Keys.SelectMany(key => dict[key]))
        {
            if (unit.unitData.hasReachDestination && !Utilities.InsideCameraBounds(Camera.main, unit.transform.position)) pool.Enqueue(unit);
            else unit.UpdateUnit();
        }
        while (pool.Count > 0)
        {
            Unit depool = pool.Dequeue();
            RemoveAndDestroy(dict, depool.gameObject.name.Split('(')[0], depool);
        }
    }
    private void Add(string type, Unit unit)
    {
        if (type.Equals(null))
        {
            LogWarning("String argument is null");
            return;
        }
        else if (!unit)
        {
            LogWarning("Unit argument is null");
            return;
        }
        else if (UnitsDict.ContainsKey(type))
        {
            UnitsDict[type].Add(unit);
        }
        else
        {
            UnitsDict.Add(type, new HashSet<Unit>());
            UnitsDict[type].Add(unit);
        }
    }
    private void RemoveAndDestroy(Dictionary<string, HashSet<Unit>> dict, string key, Unit unit)
    {
        if (key.Equals(null))
        {
            LogWarning("String argument is null");
            return;
        }
        else if (!unit)
        {
            LogWarning("Unit argument is null");
            return;
        }
        else
        {
            dict[key].Remove(unit);
            GameObject.Destroy(unit.gameObject);
        }
    }
    private void LogWarning(string msg) => Debug.LogWarning("[Unit Manager] : " + msg);
    #endregion

    public void PreInitializeUnitManager()
    {
        UnitsDict = new Dictionary<string, HashSet<Unit>>();
        UnitPool = new Queue<Unit>();
        Units = ResourcesLoader.ResourcesLoading(Globals.unitsPrefabs);
    }
    public void UpdateUnitManager() => UpdateUnits(UnitsDict, UnitPool);
}
