using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableUnitCubicBezierB : IMoveable
{
    public Vector3 Move(float angle, float speed, float t, params Vector3[] pos)
    {
        return BezierCurve.CubicBezierCurve(pos[0], pos[1], pos[2], pos[3], t);
    }
}
