using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FactoryManager : FactoryAbs
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

    GameObject[] bullets;

    public override IFactory FactoryMethod<T>(string type, Transform parent, Vector2 pos)
    {
        if (ObjectPool.Bullets[type].Count > 0)
        {
            IFactory bullet = ObjectPool.Bullets[type].Dequeue();
            return bullet;
        }
        T newBullet = Utilities.InstanciateType<T>(GetPrefab(type), parent, pos);
        return newBullet as IFactory;
    }

    public void ResourcesLoading() => bullets = Utilities.FindResources<GameObject>("Input Path");

    public GameObject GetPrefab(string type) => bullets.Where(go => go.name == type).FirstOrDefault();
}
