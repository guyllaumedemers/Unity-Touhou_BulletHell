using UnityEngine;

public class MoveableBulletB : IMoveable
{
    public Vector3 Move(float angle, float speed, float t, params Vector3[] pos)
    {
        return pos[0] + (Vector3)Utilities.CalculateXY(angle) * speed * Time.deltaTime;
    }
}
