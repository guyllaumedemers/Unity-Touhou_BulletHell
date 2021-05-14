using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShoot
{
    public abstract void Shoot(BulletType b, LayerMask ignore);
}
