using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public abstract class Bullet : MonoBehaviour, IProduct, IPoolable
{
    [Header("Bullet Values")]
    private const float speed = 5;
    private const float rad = 2;
    private float angle;
    private IMoveable moveable = new MoveableBulletBehaviour();

    //// Bullet Update position will be different depending on the pattern => Boss, Mobs, etc...
    //// Dont forget to think about the direction in which they travel
    public virtual void UpdateBulletPosition()
    {
        transform.position = moveable.Move(transform.position, angle, speed);
    }

    //// Shoot a Bullet at the Angle => Dont forget to take into consideration the spin if it needs to rotate
    public void Shoot(float angle) => this.angle = angle;

    //// Bullets should be checking for the distance between it and the target
    public bool DistanceCheck(Vector2 pos, Vector2 target) => Vector2.Distance(pos, target) <= rad;

    private void OnBecameInvisible() => Pool();

    public void Pool()
    {
        string[] keys = gameObject.name.Split('(');
        ObjectPool.LastUpdate[keys[0]] = Time.time;
        ObjectPool.Bullets[keys[0]].Enqueue(BulletManager.Instance.Find(keys[0], this) as Bullet);
        gameObject.SetActive(false);
    }

    public void Depool() => gameObject.SetActive(true);

    public void ResetBullet(Vector2 newPos) => transform.position = newPos;
}
