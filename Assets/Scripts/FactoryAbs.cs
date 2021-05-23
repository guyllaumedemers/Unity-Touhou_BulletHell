using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FactoryAbs
{
    ////// Abstract Methods to Create a Product
    public abstract IFactory FactoryMethod<T>(string type, Transform parent, Vector2 pos) where T : class;
}