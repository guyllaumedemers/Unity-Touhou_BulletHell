using UnityEngine;

public static class Globals
{
    #region Resources Loading
    public const string bulletsPrefabs = "prefabs/Bullets";
    public const string unitsPrefabs = "prefabs/Units/";
    #endregion

    #region Transform parent name
    public static string bulletParent = "Active Bullets";
    public static string poolParent = "Pool";
    public static string waypointParent = "Waypoints";
    #endregion

    public const string bulletTypes = "Assets/Scripts/BulletTypes/";

    #region Audio Path
    public static string music_clips_path = "musics/";
    public static string sfx_clips_path = "sfx/";
    #endregion

    #region Audio Channel ref
    public static string ST_Channel = "Soundtracks";
    public static string MenuSFX_Channel = "Menuing Sound Effects";
    public static string FiringSFX_Channel = "Firing Sound Effects";
    public static string AnimationSFX_Channel = "Animation Sound Effects";
    public static string Dialogue_Channel = "Dialogues";
    #endregion

    #region Audio values
    public static int channel_lowestvalue = 80;
    public static int max_percent = 100;
    public static int temp_start_percent = 90;
    #endregion

    #region Global values
    public static float fps = 1 / 60;
    public static float trimmingInterval = 2.0f;
    public static float minWPDist = 0.1f;
    public static float idleTime = 0.5f;
    public static float initializationInterval = 0.7f;
    public static float waveInterval = 2.0f;
    public static int minBullets = 10;
    public static float hitbox = 0.5f;
    public static float u_speed = 1.0f;
    public static float orbRotationSpeed = 120.0f;
    public static float fadingTime = 2.0f;
    public static float spinningUnitRotationSpeed = -240.0f;
    public static Vector3 unit_offset = new Vector3(0.0f, 1.4f, 0.0f);
    #endregion

    #region XML File Path
    public static string XMLGameinfo = "GameInfo.xml";
    public static string XMLLevelinfo = "LevelInfo.xml";
    #endregion

    #region XML Parsing values
    public static int lsposParse = 0;
    public static int rsposParse = 3;
    public static int bothsposParse = 6;
    public static int splinesposParse = 12;
    public static int maxlengthParse = 3;
    public static int maxlengthsplineParse = 4;
    #endregion

    #region Tags
    public static string waypoint = "Waypoint";
    public static string optionMenuTag = "Options";
    public static string mainMenuTag = "Main";
    public static string pauseMenuTag = "Pause";
    public static string mainVolumeTag = "MainVolume";
    public static string sfxVolumeTag = "SFXVolume";
    public static string page = "Page";
    #endregion

    #region Dynamic Loading
    public static string waypointTable = "Waypoint";
    public static string boss = "Boss";
    public static string sunflowerFairy = "SunflowerFairy";
    public static string zombieFairy = "ZombieFairy";
    #endregion

    #region string comparison
    public static string missile = "Missile";
    #endregion
}
