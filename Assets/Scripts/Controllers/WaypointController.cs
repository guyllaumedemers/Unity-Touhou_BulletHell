using UnityEngine;

public static class WaypointController
{
    #region public functions

    public static Vector3[] GetWaypoints(bool moveInterfaceSelectIsCubic, int level, DirectionEnum spEnum)
    {
        if (moveInterfaceSelectIsCubic) return GetSplineWP(level);
        else return GetLevelWPpos(level, spEnum);
    }

    #endregion

    #region private functions

    private static Vector3[] GetLevelWPpos(int level, DirectionEnum spEnum)
    {
        return WrappingTool.CustomVec3Unwrapper(DatabaseHandler.RetrieveTableEntries<WrappingTool.CustomVectorThree>(Globals.waypointTable,
            $"WHERE Id = {level} AND Direction = '{spEnum.ToString()}'"));
    }

    private static Vector3[] GetSplineWP(int level)
    {
        return WrappingTool.CustomVec3Unwrapper(DatabaseHandler.RetrieveTableEntries<WrappingTool.CustomVectorThree>(Globals.splineTable, $"WHERE Id = {level}"));
    }

    #endregion
}
