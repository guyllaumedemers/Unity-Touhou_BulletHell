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
        if (ObjectPool.Bullets.ContainsKey(type) && ObjectPool.Bullets[type].Count > 0)
        {
            IFactory bullet = ObjectPool.Bullets[type].Dequeue();
            return bullet;
        }
        T newBullet = Utilities.InstanciateType<T>(GetPrefab(type), parent, pos);
        return newBullet as IFactory;
    }

    public void ResourcesLoading() => FactoryBullets = Utilities.FindResources<GameObject>(Globals.prefabs);

    ///// A good safety catch would be to set the sprite via code when retriving the name of the gameobject

    public GameObject GetPrefab(string type) => FactoryBullets.FirstOrDefault(go => go.name.Equals(type));

    public void PreIntilizationMethod()
    {
        ResourcesLoading();                                                 // Resources are tested and correct go.name output
        //foreach (GameObject go in FactoryBullets) Debug.Log(go.name);
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
