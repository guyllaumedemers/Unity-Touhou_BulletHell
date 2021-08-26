using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPoolable
{
    public abstract void Pool();
    public abstract void Depool();
}
