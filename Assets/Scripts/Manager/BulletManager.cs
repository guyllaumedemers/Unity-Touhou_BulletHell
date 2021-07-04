using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BulletManager : SingletonMono<BulletManager>, IFlow
{
    public Dictionary<string, HashSet<Bullet>> BulletsDict { get; private set; }
    private BulletManager() { }
    public GameObject bulletParent { get; private set; }
    // handle the removal of bullets that are out of bounds
    private Queue<IProduct> oob_bullets = new Queue<IProduct>();

    /**********************ACTIONS**************************/

    private void UpdateBullets(Dictionary<string, HashSet<Bullet>> dict, Queue<IProduct> pool)
    {
        foreach (var bullet in dict.Keys.SelectMany(key => dict[key]))
        {
            if (!Utilities.InsideCameraBounds(Camera.main, bullet.transform.position)) pool.Enqueue(bullet);
            else bullet.UpdateBulletPosition();
        }
        while (pool.Count > 0) (pool.Dequeue() as Bullet).Pool();
    }

    public void Add(string type, IProduct bullet)
    {
        if (BulletsDict.ContainsKey(type)) BulletsDict[type].Add(bullet as Bullet);
        else
        {
            BulletsDict.Add(type, new HashSet<Bullet>());
            BulletsDict[type].Add(bullet as Bullet);
        }
    }

    public IProduct RemoveFind(string type, IProduct find)
    {
        BulletsDict[type].Remove(find as Bullet);
        return find;
    }

    /**********************FLOW****************************/

    public void PreIntilizationMethod()
    {
        BulletsDict = new Dictionary<string, HashSet<Bullet>>();
        bulletParent = Utilities.InstanciateObjectParent(Globals.bulletParent, true);
    }

    public void InitializationMethod() { }

    public void UpdateMethod() => UpdateBullets(BulletsDict, oob_bullets);
}
