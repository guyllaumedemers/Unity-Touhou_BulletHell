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

    // Create a namespace for this MathTrig
    // leave a note in the namespace => Mathf takes radian to convert => we already manage the conversion from degrees into radian which is  : (PI/180) * angle;
    public static Vector2 CalculateXY(float angle) => new Vector2(Mathf.Cos((Mathf.PI / 180) * angle), Mathf.Sin((Mathf.PI / 180) * angle));

    public static GameObject InstanciateObjectParent(string name, bool status)
    {
        GameObject parent = new GameObject(name);
        parent.SetActive(status);
        return parent;
    }
}
