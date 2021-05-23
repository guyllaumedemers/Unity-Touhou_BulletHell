using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ObjectPool
{
    public static Dictionary<string, Queue<Bullet>> Bullets { get; private set; }

    public static void PreInitializeMethod() => Bullets = new Dictionary<string, Queue<Bullet>>();

    public static void Fill()
    {
        foreach(GameObject go in FactoryManager.Instance.FactoryBullets)
        {
            Bullets.Add(go.name, new Queue<Bullet>());
            Debug.Log(go.name);
        }
    }

    public static void Trim() { } //// Run a Timer on each time of bullets in the dictionnary. If the count doesnt change after period of time. Chunk in half
}
