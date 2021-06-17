using UnityEngine;

public static class BezierCurve
{
    public static Vector3 LinearBezierCurve(Vector3 p0, Vector3 p1, float t)
    {
        return p0 + t * (p1 - p0);
    }

    public static Vector3 QuadraticBezierCurve(Vector3 p0, Vector3 p1, Vector3 p2, float t)
    {
        return (1 - t) * (p0 + t * (p1 - p0)) + t * (p1 + t * (p2 - p1));
    }

    public static Vector2 CubicBezierCurve(Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3, float t)
    {
        return (1 - t) * ((1 - t) * (p0 + t * (p1 - p0)) + t * (p1 + t * (p2 - p1))) + t * ((1 - t) * (p1 + t * (p2 - p1)) + t * (p2 + t * (p3 - p2)));
    }
}
