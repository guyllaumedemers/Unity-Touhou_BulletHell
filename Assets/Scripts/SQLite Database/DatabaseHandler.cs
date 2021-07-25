using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DatabaseHandler
{
    public static Bullet[] LoadBulletTypes()
    {
        return null;
    }

    public static UnitDataContainer[] LoadUnitDataTypes()
    {
        return null;
    }

    public static IDictionary<int, List<(string, Vector2[])>> LoadWaypoints()
    {
        //TODO waypoints system direction management is to be handled differently retrieving position according to the direction the unit comes from
        //which would avoid parsing thru the array like I am currently doing
        return null;
    }

    public static IDictionary<int, Queue<(string, int)>> LoadWaves()
    {
        return null;
    }
}
