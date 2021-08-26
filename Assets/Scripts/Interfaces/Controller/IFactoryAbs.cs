using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFactoryAbs
{
    public abstract IProduct FactoryMethod<T>(string type, Transform parent, Vector2 pos) where T : class;
}
