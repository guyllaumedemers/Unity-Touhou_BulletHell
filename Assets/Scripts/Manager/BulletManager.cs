using System.Collections.Generic;
using System.Linq;

public class BulletManager : SingletonMono<BulletManager>, IFlow
{
    public Dictionary<string, Queue<Bullet>> BulletsDict { get; private set; }
    private BulletManager() { }

    private int currentID;
    private const int deltaID = 10;
    private const int maxID = 50;

    private void UpdateBullets(Dictionary<string, Queue<Bullet>> bulletsDict)
    {
        foreach (var t in bulletsDict.Keys.SelectMany(b => bulletsDict[b]))
        {
            t.UpdateBulletPosition();
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

    private void ResetID() => currentID = 0;

    /**********************FLOW****************************/

    public void PreIntilizationMethod()
    {
        BulletsDict = new Dictionary<string, Queue<Bullet>>();
        currentID = 0;
    }

    public void InitializationMethod()
    {
        throw new System.NotImplementedException();
    }

    public void UpdateMethod() => UpdateBullets(BulletsDict);
}
