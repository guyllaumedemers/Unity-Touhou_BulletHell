using System.Collections;
using System.Collections.Generic;
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

    public override IFactory FactoryMethod<T>(string type)
    {
        if (ObjectPool.Bullets[type].Count > 0)
        {
            IFactory bullet = ObjectPool.Bullets[type].Dequeue();
            return bullet;
        }
        T newBullet = BulletManager.Instance.InstanciateType<T>(null, new Vector2());
        return newBullet as IFactory;
    }
}
