using System;
using System.Collections;
using System.Collections.Generic;
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

    public static bool InsideCameraBounds(Camera cam, Vector3 pos)
    {
        var viewport = cam.WorldToViewportPoint(pos);
        if (viewport.x < 0 || viewport.x > 1) return false;
        if (viewport.y < 0 || viewport.y > 1) return false;
        return true;
    }

    public static IEnumerator Timer(float time, Action status)
    {
        while (time >= 0.0f)
        {
            time -= Time.deltaTime;
            yield return null;
        }
        status.Invoke();
    }

    public static T[] ParseArray<T>(T[] arr, int start, int length)     // works only with literalm types and struct, like Vector3
    {
        var parseArr = new List<T>();
        for (int i = start; i < start + length; ++i)
        {
            parseArr.Add(arr[i]);
        }
        return parseArr.ToArray();
    }

    public static IEnumerator Fade(SpriteRenderer sprRen, string activeBulletType)
    {
        int dir = activeBulletType != Globals.missile ? -1 : 1;
        while (activeBulletType != Globals.missile ? sprRen.color.a >= 0 : sprRen.color.a <= 1)
        {
            sprRen.color = new Color(sprRen.color.r, sprRen.color.g, sprRen.color.b, sprRen.color.a + dir * Globals.fadingTime * Time.deltaTime);
            yield return null;
        }
    }

    public static Vector3 StringParseToVector3(string str)
    {
        string[] index = str.Split('(', ',', ')');
        return new Vector3(float.Parse(index[1]), float.Parse(index[2]), float.Parse(index[3]));
    }
}
