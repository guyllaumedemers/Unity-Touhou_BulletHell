
public static class Globals
{
    ///// Resources loading
    public const string bulletsPrefabs = "prefabs/Bullets";
    public const string unitsPrefabs = "prefabs/Units/";
    ///// Bullet Type class name loading
    public const string bulletTypes = "Assets/Scripts/BulletTypes/";
    ///// FPS management
    public static float fps = 1 / 60;
    ///// Object Pooling Management - Interval of Trim function call
    public static float timeInterval = 2.0f;
    ///// Object Pooling Management - Min Object in Pool per Type
    public static int minBullets = 10;
    ///// Tag
    public static string waypoint = "Waypoint";
    ///// Hitbox value
    public static float hitbox = 0.5f;
    ///// Gameobject Initialization naming
    public static string bulletParent = "Active Bullets";
    public static string poolParent = "Pool";
    public static string waypointParent = "Waypoints";
}
