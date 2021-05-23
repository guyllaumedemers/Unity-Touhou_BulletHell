using System.Collections;
using System.Linq;
using UnityEngine;

public static class Utilities
{
    public static T InstanciateType<T>(GameObject prefab, Transform parent, Vector2 pos) where T : class
    {
        return GameObject.Instantiate(prefab, pos, Quaternion.identity, parent).GetComponent<T>();
    }

    public static T[] FindResources<T>(string path) where T : class
    {
        return Resources.LoadAll(path, typeof(T)).Cast<T>().ToArray();
    }
}
