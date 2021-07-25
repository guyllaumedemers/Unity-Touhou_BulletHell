using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public abstract class Bullet : MonoBehaviour, IProduct, IPoolable
{
    BulletDataContainer bulletData;

    #region public functions

    public void FillData(BulletDataContainer data) => bulletData = data;

    public virtual void UpdateBulletPosition() => transform.position = bulletData.moveable.Move(bulletData.angle, bulletData.speed, default, transform.position);

    public void SetIgnoredLayer(IgnoreLayerEnum layer) => bulletData.ignoredLayer = layer;

    public void SetAngle(float angle) => bulletData.angle = angle;

    public void ResetBullet(Vector2 newPos)
    {
        transform.SetParent(BulletManager.Instance.bulletParent.transform);
        transform.position = newPos;
    }

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

public struct BulletDataContainer
{
    public IMoveable moveable;
    public IgnoreLayerEnum ignoredLayer;
    public float speed;
    public float angle;
    public float dmg;

    public BulletDataContainer(IgnoreLayerEnum ignoredLayer, float speed, float angle, float dmg)
    {
        this.moveable = new MoveableBulletB();
        this.ignoredLayer = ignoredLayer;
        this.speed = speed;
        this.angle = angle;
        this.dmg = dmg;
    }
}
