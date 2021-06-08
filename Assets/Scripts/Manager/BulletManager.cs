using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BulletManager : SingletonMono<BulletManager>, IFlow
{
    public Dictionary<string, HashSet<Bullet>> BulletsDict { get; private set; }        // Hashset are unordered => how would I approach a BatchUpdate System
    private BulletManager() { }
    public float Last { get; private set; }

    /**********************ACTIONS**************************/

    private void UpdateBullets(Dictionary<string, HashSet<Bullet>> bulletsDict) => BatchUpdate(bulletsDict);

    private void BatchUpdate(Dictionary<string, HashSet<Bullet>> bulletsDict)
    {
        if (Time.time - Last > Globals.fps)
        {
            foreach (var b in bulletsDict.Keys.SelectMany(key => bulletsDict[key])) b.UpdateBulletPosition();
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
    }

    public void InitializationMethod() { }

    public void UpdateMethod() => UpdateBullets(BulletsDict);
}
