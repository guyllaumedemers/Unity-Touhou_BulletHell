using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitManager : SingletonMono<UnitManager>, IFlow
{
    public Dictionary<string, HashSet<Unit>> UnitsDict { get; private set; }
    public GameObject[] Units { get; private set; }

    private Queue<Unit> UnitPool;

    #region Unit Manager Functions

    //TODO Upon loading level after Menu Selection, PreLoad Units inside a Unit Pool
    public Unit Create<T>(string type, Vector2 pos, IMoveable move_behaviour, Vector3[] waypoints, BulletTypeEnum bulletT) where T : class
    {
        Unit instance = Utilities.InstanciateType<T>(ResourcesLoader.GetPrefab(Units, type), null, pos) as Unit;
        Add(type, instance);
        return instance.PreInitializeUnit(type, move_behaviour, waypoints, bulletT);
    }

    private void UpdateUnits(Dictionary<string, HashSet<Unit>> dict, Queue<Unit> pool)
    {
        foreach (var unit in dict.Keys.SelectMany(key => dict[key]))
        {
            if (unit.GetUnitData().hasReachDestination && !Utilities.InsideCameraBounds(Camera.main, unit.transform.position)) pool.Enqueue(unit);
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
        if (UnitsDict.ContainsKey(type)) UnitsDict[type].Add(unit);
        else
        {
            UnitsDict.Add(type, new HashSet<Unit>());
            UnitsDict[type].Add(unit);
        }
    }

    private void RemoveAndDestroy(Dictionary<string, HashSet<Unit>> dict, string key, Unit unit)
    {
        dict[key].Remove(unit);
        Destroy(unit.gameObject);
    }

    //TODO Handle offset position when instancing rapidly on the same side OR make it follow in line
    //Currently following in line
    public IEnumerator SequencialInit<T>(string name, IMoveable move_behaviour, Vector3 pos, BulletTypeEnum bulletType, Vector3[] waypoints, int maxUnitWave, float interval)
        where T : class
    {
        int curr_count = -1;
        while (++curr_count < maxUnitWave)
        {
            Create<T>(name, pos, move_behaviour, waypoints, bulletType);
            yield return new WaitForSeconds(interval);
        }
    }

    #endregion

    #region Unity Functions

    public void PreIntilizationMethod()
    {
        UnitsDict = new Dictionary<string, HashSet<Unit>>();
        UnitPool = new Queue<Unit>();
        Units = ResourcesLoader.ResourcesLoading(Globals.unitsPrefabs);
    }

    public void InitializationMethod() { }

    public void UpdateMethod() => UpdateUnits(UnitsDict, UnitPool);

    #endregion
}
