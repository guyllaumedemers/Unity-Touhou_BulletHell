using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BulletManager
{
    private static BulletManager instance;
    private BulletManager() { }
    public static BulletManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new BulletManager();
            }
            return instance;
        }
    }

    public Dictionary<string, HashSet<Bullet>> BulletsDict { get; private set; }
    public GameObject bulletParent { get; private set; }
    // handle the removal of bullets that are out of bounds
    private Queue<Bullet> oob_bullets;
    private Camera gameview;

    public void Add(string type, Bullet bullet)
    {
        if (type.Equals(null))
        {
            LogWarning("Add - Argument string is null");
            return;
        }
        else if (!bullet)
        {
            LogWarning("Add - Argument Bullet is null");
            return;
        }
        else if (BulletsDict.ContainsKey(type))
        {
            BulletsDict[type].Add(bullet);
        }
        else
        {
            BulletsDict.Add(type, new HashSet<Bullet>());
            BulletsDict[type].Add(bullet);
        }
    }

    public Bullet RemoveFind(string type, Bullet bullet)
    {
        if (type.Equals(null))
        {
            LogWarning("Remove and Find - Argument string is null");
            return null;
        }
        else if (!bullet)
        {
            LogWarning("Remove and Find - Argument Bullet is null");
            return null;
        }
        else if (BulletsDict.ContainsKey(type))
        {
            BulletsDict[type].Remove(bullet);
            return bullet;
        }
        LogWarning("Remove and Find - Type cannot be found inside the dictionnary " + bullet.bulletData.bulletType);
        return null;
    }

    private void UpdateBullets(Dictionary<string, HashSet<Bullet>> dict, Queue<Bullet> pool)
    {
        foreach (var bullet in dict.Keys.SelectMany(key => dict[key]))
        {
            if (!Utilities.InsideCameraBounds(gameview, bullet.transform.position)) pool.Enqueue(bullet);
            else bullet.UpdateBulletPosition();
        }
        while (pool.Count > 0) (pool.Dequeue()).Pool();
    }

    public void PreInitializeBulletManager()
    {
        BulletsDict = new Dictionary<string, HashSet<Bullet>>();
        oob_bullets = new Queue<Bullet>();
        bulletParent = Utilities.InstanciateObjectParent(Globals.bulletParent, true, LayerMask.NameToLayer(Globals.gameview));
        gameview = GameObject.FindObjectsOfType<Camera>().Where(x => x.gameObject.tag.Equals(Globals.gameview)).FirstOrDefault();
    }

    public void UpdateBulletManager() => UpdateBullets(BulletsDict, oob_bullets);

    private void LogWarning(string msg) => Debug.LogWarning("[Bullet Manager] : " + msg);
}
