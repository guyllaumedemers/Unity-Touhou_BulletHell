using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class ObjectPool
{
    public static Dictionary<string, Queue<Bullet>> Bullets { get; private set; }

    public static Dictionary<string, float> LastUpdate { get; set; }

    public static void PreInitializeMethod()
    {
        Bullets = new Dictionary<string, Queue<Bullet>>();
        LastUpdate = new Dictionary<string, float>();
    }

    public static void Fill()
    {
        foreach (var go in FactoryManager.Instance.FactoryBullets) Bullets.Add(go.name, new Queue<Bullet>());
        foreach (var go in FactoryManager.Instance.FactoryBullets) LastUpdate.Add(go.name, Time.time);
    }

    public static IEnumerator Trim()
    {
        while (true)
        {
            foreach (var key in Bullets.Keys.Where(key => Time.time - LastUpdate[key] > Globals.timeInterval))
            {
                int count = Bullets[key].Count / 2;
                while (count < Bullets[key].Count && Globals.minBullets <= Bullets[key].Count)
                {
                    IFactory bullet = Bullets[key].Dequeue();
                    GameObject.Destroy((bullet as Bullet).gameObject);
                }
            }
            yield return new WaitForSeconds(Globals.timeInterval);
        }
    }
}
