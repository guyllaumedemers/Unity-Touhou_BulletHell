using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public Vector2 Pos { get => transform.position; }

    public void SetPosition(Vector2 pos) => transform.position = pos;
}
