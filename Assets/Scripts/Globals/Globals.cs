using UnityEngine;

public static class Globals
{
    #region Resources Loading
    public const string bulletsPrefabs = "prefabs/Bullets";
    public const string unitsPrefabs = "prefabs/Units/";
    public const string shader = "shaders/";
    public const string blurMat = "BlurMat";
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
    public static string Main_Channel = "Master";
    public static string SE_Channel = "SE";
    #endregion

    #region Audio values
    public static int channel_lowestvalue = 80;
    public static int channel_default = 0;
    public static int max_percent = 100;
    #endregion

    #region Global values
    public static float fps = 1 / 60;
    public static float blinkingTime = 0.05f;
    public static float buzzingTime = 0.1f;
    public static float slidingTime = 0.5f;
    public static float staircaseTime = 0.4f;
    public static float nextSFXTime = 0.2f;
    public static float sliding_offset = 1000.0f;
    public static float pageAnimationWaitTime = 0.7f;
    public static float trimmingInterval = 2.0f;
    public static float minWPDist = 0.1f;
    public static float idleTime = 0.5f;
    public static float sceneDelay = 1.0f;
    public static float initializationInterval = 0.7f;
    public static float waveInterval = 2.0f;
    public static int minBullets = 10;
    public static float hitbox = 0.5f;
    public static float u_speed = 1.0f;
    public static float orbRotationSpeed = 120.0f;
    public static float fadingTime = 2.0f;
    public static float curtainfade = 8.0f;
    public static float spinningUnitRotationSpeed = -240.0f;
    public static Vector3 unit_offset = new Vector3(0.0f, 1.4f, 0.0f);
    #endregion

    #region Tags
    public static string waypoint = "Waypoint";
    public static string optionMenuTag = "Options";
    public static string mainMenuTag = "Main";
    public static string pauseMenuTag = "Pause";
    public static string mainVolumeTag = "MainVolume";
    public static string sfxVolumeTag = "SFXVolume";
    public static string page = "Page";
    public static string onStartupDefault = "OnStartupDefault";
    public static string textcurtain = "TextCurtain";
    public static string uicurtain = "UICurtain";
    public static string toggleMenuComponents = "ToggleMenuComponent";
    public static string slidingComponent = "SlidingComponent";
    public static string on = "On";
    public static string off = "Off";
    #endregion

    #region Dynamic Loading
    public static string waypointTable = "Waypoint";
    public static string waveTable = "Wave";
    public static string splineTable = "Spline";
    public static string boss = "Boss";
    public static string sunflowerFairy = "SunflowerFairy";
    public static string zombieFairy = "ZombieFairy";
    #endregion

    #region string comparison
    public static string missile = "Missile";
    #endregion
}
