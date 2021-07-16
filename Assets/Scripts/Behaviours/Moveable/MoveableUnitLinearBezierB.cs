using UnityEngine;

public class MoveableUnitLinearBezierB : IMoveable
{
    public Vector3 Move(float angle, float speed, float t, params Vector3[] pos)
    {
        return BezierCurve.LinearBezierCurve(pos[0], pos[1], t);
    }
}
