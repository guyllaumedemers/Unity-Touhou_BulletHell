using UnityEngine;

public class FactoryManager : SingletonMono<FactoryManager>, IFactoryAbs, IFlow
{
    private FactoryManager() { }
    public GameObject[] FactoryBullets { get; private set; }

    #region Facotry Manager Functions

    public IProduct FactoryMethod<T>(string type, Transform parent, Vector2 pos) where T : class
    {
        IProduct bullet;
        if (ObjectPool.Bullets.ContainsKey(type) && ObjectPool.Bullets[type].Count > 0)
        {
            bullet = ObjectPool.Bullets[type].Dequeue();
            (bullet as Bullet).ResetBullet(pos);
            goto SKIP;
        }
        bullet = Utilities.InstanciateType<T>(ResourcesLoader.GetPrefab(FactoryBullets, type), parent, pos) as IProduct;
        //TODO Retrieve bullet data depending on the type pass as arguments and fill in the BulletDataContainer
        (bullet as Bullet).FillData(new BulletDataContainer());
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
