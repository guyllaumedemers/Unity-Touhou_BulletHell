using UnityEngine;

public class MoveableUnitLinearBezierB : IMoveable
{
    public Vector3 Move(float angle, float speed, float t, params Vector3[] pos)
    {
        if (pos.Length < 2)
        {
            LogWarning("The params Array is missing positions");
            return Vector3.zero;
        }
        return BezierCurve.LinearBezierCurve(pos[0], pos[1], t);
    }

    private void LogWarning(string msg) => Debug.LogWarning("[Moveable Bezier Linear] " + msg);
}
