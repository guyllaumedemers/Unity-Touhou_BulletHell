using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public abstract class Bullet : MonoBehaviour, IFactory
{
    [Header("Components")]
    public SpriteRenderer ren;
    public Sprite img;

    [Header("Bullet Values")]
    public Vector2 pos;
    public float rad;

    //// BulletType are going to define their own Shoot function
    public abstract void Shoot();

    //// Bullets should be checking for the distance between it and the target
    public bool DistanceCheck(Vector2 pos, Vector2 target, float rad)
    {
        return Vector2.Distance(pos, target) <= rad;
    }
}
