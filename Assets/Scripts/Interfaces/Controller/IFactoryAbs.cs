using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFactoryAbs
{
    public abstract Bullet FactoryMethod(string type, Transform parent, Vector2 pos);
}
