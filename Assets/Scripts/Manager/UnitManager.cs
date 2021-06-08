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
    public Unit Create<T>(string type, Vector2 pos) where T : class
    {
        Unit instance = Utilities.InstanciateType<T>(resources.GetPrefab(Units, type), null, pos) as Unit;
        Add(type, instance);
        return instance.PreInitializeUnit();
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

    /**********************FLOW****************************/

    public void PreIntilizationMethod()
    {
        UnitsDict = new Dictionary<string, HashSet<Unit>>();
        Units = resources.ResourcesLoading(Globals.unitsPrefabs);
    }

    public void InitializationMethod() { }

    public void UpdateMethod() => UpdateUnits(UnitsDict);
}
