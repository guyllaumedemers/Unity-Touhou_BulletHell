using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitManager : SingletonMono<UnitManager>, IFlow
{
    public Dictionary<string, HashSet<Unit>> UnitsDict { get; private set; }
    public GameObject[] Units { get; private set; }
    private UnitManager() { }
    private readonly IResourcesLoading resources = new ResourcesLoadingBehaviour();

    /**********************ACTIONS**************************/

    ///// Unit creation is not handle inside the Factory Pattern
    ///// Factory Pattern is only going to handle bullet instanciation as there wont be that many units on screen
    public Unit Create<T>(string type, Vector2 pos, BulletTypeEnum bulletT, Vector3[] waypoints) where T : class
    {
        Unit instance = Utilities.InstanciateType<T>(resources.GetPrefab(Units, type), null, pos) as Unit;
        Add(type, instance);
        return instance.PreInitializeUnit(bulletT, waypoints);
    }

    private void UpdateUnits(Dictionary<string, HashSet<Unit>> unitsDict)
    {
        foreach (var unit in unitsDict.Keys.SelectMany(key => unitsDict[key])) unit.UpdateUnit();
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

    private IEnumerator SequencialInit<T>(string name, Vector3 pos, BulletTypeEnum bulletType, int level, int maxUnitWave, float interval) where T : class
    {
        int curr_count = -1;
        while (++curr_count < maxUnitWave)
        {
            Create<T>(name, pos, bulletType, WaypointSystem.Instance.GetLevelWPpos(level));
            yield return new WaitForSeconds(interval);
        }
    }

    /**********************FLOW****************************/

    public void PreIntilizationMethod()
    {
        UnitsDict = new Dictionary<string, HashSet<Unit>>();
        Units = resources.ResourcesLoading(Globals.unitsPrefabs);
    }

    // Unit Manager will handle which side the units are attract to
    public void InitializationMethod()
    {
        StartCoroutine(SequencialInit<Boss>("Boss", Vector3.one, BulletTypeEnum.Circle | BulletTypeEnum.Star, 0, 3, 1.0f));
    }

    public void UpdateMethod() => UpdateUnits(UnitsDict);


}
