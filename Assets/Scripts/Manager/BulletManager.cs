using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BulletManager : SingletonMono<BulletManager>, IFlow
{
    public Dictionary<string, Queue<Bullet>> BulletsDict { get; private set; }
    private BulletManager() { }
    public IFactory CurrentBullet { get; private set; }

    private void UpdateBullets(Dictionary<string, Queue<Bullet>> bulletsDict)
    {
        foreach (var b in bulletsDict.Keys.SelectMany(key => bulletsDict[key]))       // Update every bullet for every type
        {
            b.UpdateBulletPosition();
        }
        //foreach (var key in bulletsDict.Keys) BatchUpdate(bulletsDict, key, 0);
    }

    public void Add(string type, IFactory bullet)
    {
        if (BulletsDict.ContainsKey(type)) BulletsDict[type].Enqueue(bullet as Bullet);
        else
        {
            BulletsDict.Add(type, new Queue<Bullet>());
            BulletsDict[type].Enqueue(bullet as Bullet);
        }
    }

    private void BatchUpdate(Dictionary<string, Queue<Bullet>> bulletsDict, string key, int cpt)
    {
        if (bulletsDict[key].Count == 0 || cpt >= Globals.minBullets)
            return;
        {
            IFactory bullet = bulletsDict[key].Dequeue();
            CurrentBullet = bullet;                         // Store current bullet depooled so that if the position is outside the viewport
                                                            // and pool is called we can ref this bullet
            (bullet as Bullet).UpdateBulletPosition();
            if ((bullet as Bullet).gameObject.activeSelf) bulletsDict[key].Enqueue(bullet as Bullet);
        }
        BatchUpdate(bulletsDict, key, ++cpt);
    }

    /**********************FLOW****************************/

    public void PreIntilizationMethod() => BulletsDict = new Dictionary<string, Queue<Bullet>>();

    public void InitializationMethod() { }

    public void UpdateMethod() => UpdateBullets(BulletsDict);
}
