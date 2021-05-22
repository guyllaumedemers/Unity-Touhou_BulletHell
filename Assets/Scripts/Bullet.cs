using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public abstract class Bullet : MonoBehaviour, IFactory, IPoolable
{
    [Header("Bullet Values")]
    private Vector2 pos;
    private float speed;
    [Header("Collision Detection Values")]
    private float rad;

    public bool ID { get; private set; }

    //// Bullet Update position will be different depending on the pattern => Boss, Mobs, etc...
    public virtual void UpdateBulletPosition()
    {
        transform.position += new Vector3(pos.x, pos.y, 0) * speed * Time.deltaTime;
    }

    //// BulletType are going to define their own Shoot function
    public abstract void Shoot();

    //// Bullets should be checking for the distance between it and the target
    public bool DistanceCheck(Vector2 pos, Vector2 target, float rad)
    {
        return Vector2.Distance(pos, target) <= rad;
    }

    private void OnBecameInvisible()
    {
        Pool();
    }

    public void Pool()
    {
        ObjectPool.Bullets[this.ToString()].Enqueue(this);
        BulletManager.Instance.BulletsDict[this.ToString()].Dequeue();
        gameObject.SetActive(false);
    }

    public void Depool()
    {
        IFactory bullet = ObjectPool.Bullets[this.ToString()].Dequeue();
        BulletManager.Instance.BulletsDict[this.ToString()].Enqueue(bullet as Bullet);
        gameObject.SetActive(true);
    }

    public void ResetBullet(Vector2 pos)
    {

    }
}
