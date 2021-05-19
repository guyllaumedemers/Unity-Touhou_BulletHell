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
            if(instance == null)
            {
                instance = new FactoryManager();
            }
            return instance;
        }
    }
    #endregion

    public override IFactory FactoryMethod<T>(string type)
    {
        ////// The Player is going to ask to the FactoryManager if he can have a Bullet
        ////// FactoryManager will be looking at the Object Pool => do you have the bullet of type
        ////// Object Pool will return OR not
        ////// if not FactoryManager will Instanciate
        return null;
    }
}
