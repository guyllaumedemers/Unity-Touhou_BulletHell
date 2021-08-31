using System.Linq;
using UnityEngine;

public class FactoryManager : IFactoryAbs
{
    private static FactoryManager instance = null;
    private FactoryManager() { }
    public static FactoryManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new FactoryManager();
            }
            return instance;
        }
    }

    public GameObject[] FactoryBullets { get; private set; }

    public Bullet FactoryMethod(string type, Transform parent, Vector2 pos)
    {
        Bullet bullet;
        if (ObjectPoolController.Instance.Bullets.ContainsKey(type) && ObjectPoolController.Instance.Bullets[type].Count > 0)
        {
            bullet = ObjectPoolController.Instance.Bullets[type].Dequeue();
            bullet.ResetBullet(parent, pos);
            goto SKIP;
        }
        bullet = Utilities.InstanciateType<Bullet>(ResourcesLoader.GetPrefab(FactoryBullets, type), parent, pos);
        bullet.FillData(DatabaseHandler.RetrieveTableEntries<BulletDataContainer>(SQLTableEnum.Bullet.ToString()).Where(x => x.bulletType.ToString().Equals(type)).FirstOrDefault());
    SKIP:
        BulletManager.Instance.Add(type, bullet);
        return bullet;
    }

    public void PreInitializeFactoryPrefabs() => FactoryBullets = ResourcesLoader.ResourcesLoading(Globals.bulletsPrefabs);
}
