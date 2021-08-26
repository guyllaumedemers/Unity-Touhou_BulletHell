using UnityEngine;

public interface IMoveable
{
    public abstract Vector3 Move(float angle, float speed, float t, params Vector3[] pos);
}
