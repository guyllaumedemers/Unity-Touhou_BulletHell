using System.Collections.Generic;
using Unity.Linq;
using UnityEngine;

public class WaypointSystem : SingletonMono<WaypointSystem>, IFlow
{
    private WaypointSystem() { }
    public IDictionary<int, Vector3[]> positions;

    #region public functions

    //INFO SpawningPosEnum is not taken into consideration on the WaveSystem side as it isnt part of the update function for setting the SpawningEnum used for managing sides
    //WHY? Because I want to be able to use spline while being able to set units from both side of the screens
    //I will only have to flip the starting pos of the unit using the spline depending on the side the Enum so the unit start at the proper position outside the screen
    public Vector3[] GetWaypoints(bool moveInterfaceSelectIsCubic, int level, SpawningPosEnum spEnum)
    {
        if (moveInterfaceSelectIsCubic) return GetLevelWPpos(level, SpawningPosEnum.None);
        else return GetLevelWPpos(level, spEnum);
    }

    #endregion


    #region private functions

    private Vector3[] GetLevelWPpos(int level, SpawningPosEnum spEnum) => spEnum switch
    {
        SpawningPosEnum.Left => Utilities.ParseArray(positions[level], Globals.lsposParse, Globals.maxlengthParse),
        SpawningPosEnum.Right => Utilities.ParseArray(positions[level], Globals.rsposParse, Globals.maxlengthParse),
        SpawningPosEnum.Both => Utilities.ParseArray(positions[level], Globals.bothsposParse, Globals.maxlengthParse * 2),
        SpawningPosEnum.None => Utilities.ParseArray(positions[level], Globals.splinesposParse, Globals.maxlengthsplineParse),
        _ => throw new System.ArgumentOutOfRangeException()
    };

    #endregion

    #region Unity Functions

    public void PreIntilizationMethod()
    {
        positions = Tool.XMLDeserialization_KVParray(Globals.XMLGameinfo);
    }

    public void InitializationMethod() { }

    public void UpdateMethod() { }

    #endregion
}
