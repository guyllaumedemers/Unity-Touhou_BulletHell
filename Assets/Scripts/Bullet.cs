using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public abstract class Bullet : MonoBehaviour, IProduct, IPoolable
{
    IMoveable moveable = new MoveableBulletB();
    protected const float speed = 5;
    protected float angle;
    public IgnoreLayerEnum ignoredLayer { get; private set; }
    public float dmg { get; private set; }

    #region Bullet Functions

    public virtual void UpdateBulletPosition() => transform.position = moveable.Move(angle, speed, default, transform.position);

    public void SetIgnoredLayer(IgnoreLayerEnum layer) => ignoredLayer = layer;

    public void SetAngle(float angle) => this.angle = angle;

    public void ResetBullet(Vector2 newPos)
    {
        transform.SetParent(BulletManager.Instance.bulletParent.transform);
        transform.position = newPos;
    }

    #endregion

    #region Pooling Functions

    public void Pool()
    {
        string[] keys = gameObject.name.Split('(');
        ObjectPool.LastUpdate[keys[0]] = Time.time;
        ObjectPool.Bullets[keys[0]].Enqueue(BulletManager.Instance.RemoveFind(keys[0], this) as Bullet);
        gameObject.transform.SetParent(ObjectPool.pool.transform);
    }

    public void Depool() { }

    #endregion
}
