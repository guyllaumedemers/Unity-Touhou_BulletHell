using UnityEngine;

public static class WaypointSystem
{
    #region public functions

    public static Vector3[] GetWaypoints(bool moveInterfaceSelectIsCubic, int level, SpawningPosEnum spEnum)
    {
        if (moveInterfaceSelectIsCubic) return GetLevelWPpos(level, SpawningPosEnum.None);
        else return GetLevelWPpos(level, spEnum);
    }

    #endregion

    #region private functions

    private static Vector3[] GetLevelWPpos(int level, SpawningPosEnum spEnum)
    {
        //TODO Filter the database so we only retrieve the values for the level AND for the direction enum
        return Tool.CustomVec3Unwrapper(DatabaseHandler.RetrieveTableEntries<Tool.Vector3Wrapper>(Globals.waypointTable, $"WHERE Id = {level} AND Direction = '{spEnum.ToString()}'"));
    }

    #endregion
}
