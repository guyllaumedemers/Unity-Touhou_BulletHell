using UnityEngine;

public static class WaypointSystem
{
    #region public functions

    public static Vector3[] GetWaypoints(bool moveInterfaceSelectIsCubic, int level, SpawningPosEnum spEnum)
    {
        if (moveInterfaceSelectIsCubic) return GetSplineWP(level);
        else return GetLevelWPpos(level, spEnum);
    }

    #endregion

    #region private functions

    private static Vector3[] GetLevelWPpos(int level, SpawningPosEnum spEnum)
    {
        return Tool.CustomVec3Unwrapper(DatabaseHandler.RetrieveTableEntries<Tool.Vector3Wrapper>(Globals.waypointTable,
            $"WHERE Id = {level} AND Direction = '{spEnum.ToString()}'"));
    }

    private static Vector3[] GetSplineWP(int level)
    {
        return Tool.CustomVec3Unwrapper(DatabaseHandler.RetrieveTableEntries<Tool.Vector3Wrapper>(Globals.splineTable, $"WHERE Id = {level}"));
    }

    #endregion
}
