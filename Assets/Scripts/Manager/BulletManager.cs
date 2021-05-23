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
        if (currentID >= maxID) ResetID();
        ///// Still have to manage a way to avoid errors when removing bullets so the loop doesnt break
        foreach (var bullet in bulletsDict.Keys.SelectMany(key => bulletsDict[key].Where(b => b.ID >= currentID && b.ID <= currentID + deltaID)))
        {
            bullet.UpdateBulletPosition();
        }
        currentID += deltaID;
    }

    private void ResetID() => currentID = 0;

    /**********************FLOW****************************/

    public void PreIntilizationMethod()
    {
        ///// How it is going to work
        ///// Instead of keeping track of BulletType, we are going to put into a single folder all sub-script
        ///// defining a bullet type and Resources.LoadAll inside the dependency in order to retrieve their string name onStart\
        ///// and fill the dictionnary
        BulletsDict = new Dictionary<string, Queue<Bullet>>();
        currentID = 0;
    }

    public void InitializationMethod()
    {
        throw new System.NotImplementedException();
    }

    public void UpdateMethod()
    {
        ///// BulletManager is going Update Bullets every cycle
        UpdateBullets(BulletsDict);
    }
}
