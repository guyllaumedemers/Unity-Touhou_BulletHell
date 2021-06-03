
public static class Globals
{
    ///// Resources loading
    public const string bulletsPrefabs = "prefabs/Bullets";
    public const string unitsPrefabs = "prefabs/Units/";
    ///// Bullet Type class name loading
    public const string bulletTypes = "Assets/Scripts/BulletTypes/";
    ///// FPS management
    public static float fps = 1/60;
    ///// Object Pooling Management - Interval of Trim function call
    public static float timeInterval = 4.0f;
    ///// Object Pooling Management - Min Object in Pool per Type
    public static int minBullets = 10;
}
