
public static class Globals
{
    ///// Resources loading
    public const string prefabs = "prefabs/";
    ///// Bullet Type class name loading
    public const string bulletTypes = "Assets/Scripts/BulletTypes/";
    ///// Bullet Type class name loading
    public static int bulletID = 0;
    ///// Object Pooling Management - Interval of Trim function call
    public static float purge = 4.0f;
    ///// Object Pooling Management - Min Object in Pool per Type
    public static int minObject = 10;
}
