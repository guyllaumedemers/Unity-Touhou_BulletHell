using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public abstract class Bullet : MonoBehaviour, IProduct, IPoolable
{
    [Header("Bullet Values")]
    private const float speed = 5;
    private const float rad = 2;
    private float angle;

    //// Bullet Update position will be different depending on the pattern => Boss, Mobs, etc...
    //// Dont forget to think about the direction in which they travel
    public virtual void UpdateBulletPosition()
    {
        transform.position += (Vector3)Utilities.CalculateXY(angle) * speed * Time.deltaTime;
    }

    //// Shoot a Bullet at the Angle => Dont forget to take into consideration the spin if it needs to rotate
    public void Shoot(float angle) => this.angle = angle;

    //// Bullets should be checking for the distance between it and the target
    public bool DistanceCheck(Vector2 pos, Vector2 target)
    {
        return Vector2.Distance(pos, target) <= rad;
    }

    private void OnBecameInvisible() => Pool();

    public void Pool()
    {
        string[] keys = gameObject.name.Split('(');
        ObjectPool.LastUpdate[keys[0]] = Time.time;
        ObjectPool.Bullets[keys[0]].Enqueue(BulletManager.Instance.BulletsDict[keys[0]].Dequeue());
        //ObjectPool.Bullets[keys[0]].Enqueue(BulletManager.Instance.CurrentBullet as Bullet);
        gameObject.SetActive(false);
    }

    public void Depool() => gameObject.SetActive(true);

    public void ResetBullet(Vector2 newPos) => transform.position = newPos;
}
