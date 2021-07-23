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
    public static float minWPDist = 0.1f;
    ///// Idl Time
    public static float idleTime = 0.5f;
    ///// Left Parse start pos
    public static int lsposParse = 0;
    ///// Right Parse start pos
    public static int rsposParse = 3;
    ///// Both Parse start pos
    public static int bothsposParse = 6;
    ///// Spline Parse start pos
    public static int splinesposParse = 12;
    ///// Parse arr max length
    public static int maxlengthParse = 3;
    ///// Parse arr max length Splice
    public static int maxlengthsplineParse = 4;
    ///// Sequential init time interval
    public static float initializationInterval = 0.7f;
    ///// Wave time interval
    public static float waveInterval = 2.0f;
    ///// Unit Generation
    public static string boss = "Boss";
    public static string sunflowerFairy = "SunflowerFairy";
    public static string zombieFairy = "ZombieFairy";
    public static string player = "Player";
    ///// Unit Speed
    public static float u_speed = 1.0f;
    ///// Orb Rotation
    public static float orbRotationSpeed = 120.0f;
    ///// Orb Fade
    public static float fadingTime = 2.0f;
    ///// Spinning Unit Rotation
    public static float spinningUnitRotationSpeed = -240.0f;
    ///// string comparison - player
    public static string missile = "Missile";
    ///// filename
    public static string XMLGameinfo = "GameInfo.xml";
    public static string XMLLevelinfo = "LevelInfo.xml";
    ///// Instanciation position offset
    public static Vector3 unit_offset = new Vector3(0.0f, 1.4f, 0.0f);
    ///// Tags
    public static string optionMenuTag = "Options";
    public static string mainMenuTag = "Main";
    public static string pauseMenuTag = "Pause";
    public static string mainVolumeTag = "MainVolume";
    public static string sfxVolumeTag = "SFXVolume";
    ///// Audio Channels
    public static string music_channel = "Music";
    public static string sfx_channel = "SFX";
    public static int channel_lowestvalue = 80;
    public static int percent = 100;
}
