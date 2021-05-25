using System.Linq;
using UnityEngine;

public class FactoryManager : FactoryAbs, IFlow
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

    public override IFactory FactoryMethod<T>(string type, Transform parent, Vector2 pos)
    {
        IFactory bullet;
        if (ObjectPool.Bullets.ContainsKey(type) && ObjectPool.Bullets[type].Count > 0)
        {
            bullet = ObjectPool.Bullets[type].Dequeue();
            (bullet as Bullet).Depool();
            goto SKIP;
        }
        bullet = Utilities.InstanciateType<T>(GetPrefab(type), parent, pos) as IFactory;
    SKIP:
        (bullet as Bullet).ResetBullet(pos);
        BulletManager.Instance.Add(type, bullet);
        return bullet;
    }

    public void ResourcesLoading() => FactoryBullets = Utilities.FindResources<GameObject>(Globals.prefabs);

    ///// A good safety catch would be to set the sprite via code when retriving the name of the gameobject

    public GameObject GetPrefab(string type) => FactoryBullets.FirstOrDefault(go => go.name.Equals(type));

    /**********************FLOW****************************/

    public void PreIntilizationMethod()
    {
        ResourcesLoading();
    }

    public void InitializationMethod()
    {
        throw new System.NotImplementedException();
    }

    public void UpdateMethod()
    {
        throw new System.NotImplementedException();
    }
}
