using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFactory
{
    /// This is an interface for retrieving the Object and the methods it implements
    public abstract void Shoot(float angle);
}
