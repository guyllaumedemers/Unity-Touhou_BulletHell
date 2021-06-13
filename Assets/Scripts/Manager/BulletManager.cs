using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BulletManager : SingletonMono<BulletManager>, IFlow
{
    public Dictionary<string, HashSet<Bullet>> BulletsDict { get; private set; }
    private BulletManager() { }
    public float Last { get; private set; }
    public GameObject bulletParent;
    private Queue<IProduct> products = new Queue<IProduct>();

    /**********************ACTIONS**************************/

    private void UpdateBullets(Dictionary<string, HashSet<Bullet>> bulletsDict) => BatchUpdate(bulletsDict);

    private void BatchUpdate(Dictionary<string, HashSet<Bullet>> bulletsDict)
    {
        if (Time.time - Last > Globals.fps)
        {
            foreach (var b in bulletsDict.Keys.SelectMany(key => bulletsDict[key]))
            {
                if (!Utilities.InsideCameraBounds(Camera.main, b.transform.position)) products.Enqueue(b);
                else b.UpdateBulletPosition();
            }
            while (products.Count > 0) (products.Dequeue() as Bullet).Pool();
            Last = Time.time;
        }
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
        Last = default;
        bulletParent = Utilities.InstanciateObjectParent("Active Bullets", true);
    }

    public void InitializationMethod() { }

    public void UpdateMethod() => UpdateBullets(BulletsDict);
}
