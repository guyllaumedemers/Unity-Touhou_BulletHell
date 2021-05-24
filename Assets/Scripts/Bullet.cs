using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public abstract class Bullet : MonoBehaviour, IFactory, IPoolable
{
    [Header("Bullet Values")]
    private Vector2 pos;
    private const float rad = 2;
    private const float speed = 5;
    // Batching ID
    public int ID { get; private set; }

    //// Bullet Update position will be different depending on the pattern => Boss, Mobs, etc...
    //// Dont forget to think about the direction in which they travel
    public virtual void UpdateBulletPosition()
    {
        //// try to shoot forward
    }

    //// BulletType are going to define their own Shoot function
    public abstract void Shoot();

    //// Bullets should be checking for the distance between it and the target
    public bool DistanceCheck(Vector2 pos, Vector2 target)
    {
        return Vector2.Distance(pos, target) <= rad;
    }

    private void OnBecameInvisible() => Pool();

    public void Pool()
    {
        string[] keys = gameObject.name.Split('(');
        ObjectPool.Bullets[keys[0]].Enqueue(BulletManager.Instance.BulletsDict[keys[0]].Dequeue());
        gameObject.SetActive(false);
    }

    public void Depool() => gameObject.SetActive(true);

    public void ResetBullet(Vector2 newPos) => pos = newPos;

    private void InitializeMethod() => pos = new Vector2();
}
