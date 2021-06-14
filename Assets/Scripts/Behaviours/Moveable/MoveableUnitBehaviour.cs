using UnityEngine;

public class MoveableUnitBehaviour : IMoveable
{
    public Vector3 Move(float angle, float speed, float t, params Vector3[] pos)
    {
        return BezierCurve.QuadraticBezierCurve(pos[0], pos[1], pos[2], t);
    }
}
