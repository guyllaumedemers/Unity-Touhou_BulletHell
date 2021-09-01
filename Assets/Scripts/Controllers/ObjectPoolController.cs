using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ObjectPoolController
{
    private static ObjectPoolController instance;
    private ObjectPoolController() { }
    public static ObjectPoolController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new ObjectPoolController();
            }
            return instance;
        }
    }

    public Dictionary<string, Queue<Bullet>> Bullets { get; private set; }

    public Dictionary<string, float> LastUpdate { get; set; }

    public GameObject pool;

    public void PreInitializeObjectPoolController()
    {
        Bullets = new Dictionary<string, Queue<Bullet>>();
        LastUpdate = new Dictionary<string, float>();
        pool = Utilities.InstanciateObjectParent(Globals.poolParent, false);
        Fill();
    }

    private void Fill()
    {
        foreach (var go in FactoryManager.Instance.FactoryBullets) Bullets.Add(go.name, new Queue<Bullet>());
        foreach (var go in FactoryManager.Instance.FactoryBullets) LastUpdate.Add(go.name, Time.time);
    }

    public IEnumerator Trim()
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
