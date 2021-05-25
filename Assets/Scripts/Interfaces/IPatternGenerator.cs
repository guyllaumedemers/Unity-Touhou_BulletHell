using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPatternGenerator
{
    public abstract void UpdateBulletPattern(Transform transform, float speed, float dir);
}
