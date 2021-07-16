using UnityEngine;

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
    public static float trimmingInterval = 2.0f;
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
    ///// Distance wp
    public static float minWPDist = 0.01f;
    ///// Idl Time
    public static float idleTime = 1.2f;
    ///// Left Parse start pos
    public static int lsposParse = 0;
    ///// Right Parse start pos
    public static int rsposParse = 3;
    ///// Both Parse start pos
    public static int bothsposParse = 6;
    ///// Parse arr max length
    public static int maxlengthParse = 3;
    ///// Sequential init time interval
    public static float initializationInterval = 0.7f;
    ///// Wave time interval
    public static float waveInterval = 5.0f;
    ///// Unit Generation
    public static string boss = "Boss";
    public static string sunflowerFairy = "SunflowerFairy";
    public static string zombieFairy = "ZombieFairy";
    public static string player = "Player";
    ///// Unit Speed
    public static float u_speed = 0.03f;
    ///// Orb Rotation
    public static float orbRotationSpeed = 120.0f;
    ///// Orb Fade
    public static float fadingTime = 2.0f;
    ///// string comparison - player
    public static string missile = "Missile";
    ///// filename
    public static string XMLGameinfo = "GameInfo.xml";
    public static string XMLLevelinfo = "LevelInfo.xml";
    ///// Instanciation position offset
    public static Vector3 unit_offset = new Vector3(0.0f, 0.7f, 0.0f);
}
