using UnityEngine;

public class MoveableBossBehaviour : IMoveable
{
    public Vector3 Move(Vector3 pos, float angle, float speed) => Vector3.zero;
}
