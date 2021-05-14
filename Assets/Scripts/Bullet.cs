using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Bullet : MonoBehaviour
{
    [Header("Components")]
    private SpriteRenderer ren;
    private Sprite img;

    private Vector2 pos;

    //// Bullets should be checking for the distance between it and the target
    private bool DistanceCheck(Vector2 pos, Vector2 target, float rad)
    {
        return Vector2.Distance(pos, target) <= rad;
    }
}
