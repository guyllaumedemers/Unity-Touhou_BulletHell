using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableBulletBehaviour : IMoveable
{
    public Vector3 Move(Vector3 pos, float angle, float speed) => pos + (Vector3)Utilities.CalculateXY(angle) * speed * Time.deltaTime;
}
