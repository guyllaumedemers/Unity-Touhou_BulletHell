using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissilePattern : AbsPattern
{
    public override void UpdateBulletPattern(Transform transform, float speed, float dir)
    {
        transform.position += (Vector3)Utilities.CalculateXY(dir > 0 ? 90 : 180) * speed * Time.deltaTime;
    }
}
