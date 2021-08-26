using System.Linq;
using UnityEngine;

public class FactoryManager : SingletonMono<FactoryManager>, IFactoryAbs, IFlow
{
    private FactoryManager() { }
    public GameObject[] FactoryBullets { get; private set; }

    #region Facotry Manager Functions

    public IProduct FactoryMethod<T>(string type, Transform parent, Vector2 pos) where T : class
    {
        IProduct bullet;
        if (ObjectPoolController.Bullets.ContainsKey(type) && ObjectPoolController.Bullets[type].Count > 0)
        {
            bullet = ObjectPoolController.Bullets[type].Dequeue();
            (bullet as Bullet).ResetBullet(pos);
            goto SKIP;
        }
        bullet = Utilities.InstanciateType<T>(ResourcesLoader.GetPrefab(FactoryBullets, type), parent, pos) as IProduct;
        (bullet as Bullet).FillData(DatabaseHandler.RetrieveTableEntries<BulletDataContainer>(SQLTableEnum.Bullet.ToString()).Where(x => x.bulletType.ToString().Equals(type)).FirstOrDefault());
    SKIP:
        BulletManager.Instance.Add(type, bullet);
        return bullet;
    }

    #endregion

    #region Unity Functions

    public void PreIntilizationMethod() => FactoryBullets = ResourcesLoader.ResourcesLoading(Globals.bulletsPrefabs);

    public void InitializationMethod() { }

    public void UpdateMethod() { }

    #endregion
}
