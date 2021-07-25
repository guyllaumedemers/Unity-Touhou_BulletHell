using UnityEngine;

public class MoveableBulletB : IMoveable
{
    public Vector3 Move(float angle, float speed, float t, params Vector3[] pos)
    {
        if (pos.Length < 1)
        {
            LogWarning("The params Array is Empty");
            return Vector3.zero;
        }
        return pos[0] + (Vector3)Utilities.CalculateXY(angle) * speed * Time.deltaTime;
    }

    private void LogWarning(string msg) => Debug.LogWarning("[Moveable Bullet Linear] " + msg);
}
