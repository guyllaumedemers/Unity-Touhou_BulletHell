using System.Linq;
using UnityEngine;

public class FactoryManager : IFactoryAbs, IFlow
{
    #region singleton
    private static FactoryManager instance;
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
    #endregion
    public GameObject[] FactoryBullets { get; private set; }
    private readonly IResourcesLoading resources = new ResourcesLoadingBehaviour();

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
        bullet = Utilities.InstanciateType<T>(resources.GetPrefab(FactoryBullets, type), parent, pos) as IProduct;
    SKIP:
        BulletManager.Instance.Add(type, bullet);
        return bullet;
    }

    #endregion

    #region Unity Functions

    public void PreIntilizationMethod() => FactoryBullets = resources.ResourcesLoading(Globals.bulletsPrefabs);

    public void InitializationMethod() { }

    public void UpdateMethod() { }

    #endregion
}
