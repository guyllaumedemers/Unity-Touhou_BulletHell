using System.Collections.Generic;
using System.Linq;

public class BulletManager : SingletonMono<BulletManager>, IFlow
{
    public Dictionary<string, Queue<Bullet>> BulletsDict { get; private set; }
    private BulletManager() { }

    private void UpdateBullets(Dictionary<string, Queue<Bullet>> bulletsDict)
    {
        foreach (var b in bulletsDict.Keys.SelectMany(key => bulletsDict[key]))
        {
            b.UpdateBulletPosition();
        }
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

    /**********************FLOW****************************/

    public void PreIntilizationMethod()
    {
        BulletsDict = new Dictionary<string, Queue<Bullet>>();
    }

    public void InitializationMethod()
    {
        throw new System.NotImplementedException();
    }

    public void UpdateMethod() => UpdateBullets(BulletsDict);
}
