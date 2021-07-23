using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class ObjectPool
{
    public static Dictionary<string, Queue<Bullet>> Bullets { get; private set; }

    public static Dictionary<string, float> LastUpdate { get; set; }

    public static GameObject pool;

    public static void PreInitializeMethod()
    {
        Bullets = new Dictionary<string, Queue<Bullet>>();
        LastUpdate = new Dictionary<string, float>();
        pool = Utilities.InstanciateObjectParent(Globals.poolParent, false);
        Fill();
    }

    private static void Fill()
    {
        foreach (var go in FactoryManager.Instance.FactoryBullets) Bullets.Add(go.name, new Queue<Bullet>());
        foreach (var go in FactoryManager.Instance.FactoryBullets) LastUpdate.Add(go.name, Time.time);
    }

    public static IEnumerator Trim()
    {
        while (true)
        {
            foreach (var key in Bullets.Keys.Where(key => Time.time - LastUpdate[key] > Globals.trimmingInterval))
            {
                int count = Bullets[key].Count / 2;
                while (count >= Globals.minBullets)
                {
                    GameObject.Destroy(Bullets[key].Dequeue().gameObject);
                    count--;
                }
            }
            yield return new WaitForSeconds(Globals.trimmingInterval);
        }
    }
}
