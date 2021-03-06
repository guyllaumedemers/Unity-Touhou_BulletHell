using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Bullet : MonoBehaviour
{
    public BulletDataContainer bulletData;

    #region public functions

    public void FillData(BulletDataContainer data) => bulletData = data;

    public void UpdateBulletPosition() => transform.position = bulletData.moveable.Move(bulletData.angle, bulletData.speed, default, transform.position);

    public void SetIgnoredLayer(IgnoreLayerEnum layer)
    {
        if (layer.Equals(IgnoreLayerEnum.None))
        {
            LogWarning("Invalid Layer Assignation");
            return;
        }
        bulletData.ignoredLayer = layer;
    }

    public void SetAngle(float angle) => bulletData.angle = angle;

    public void ResetBullet(Transform parent, Vector2 newPos)
    {
        if (!parent)
        {
            LogWarning("There is no parent for this bullet : " + gameObject.name);
            return;
        }
        transform.SetParent(parent);
        transform.position = newPos;
    }

    public void Pool()
    {
        string[] keys = gameObject.name.Split('(');
        ObjectPoolController.Instance.LastUpdate[keys[0]] = Time.time;
        ObjectPoolController.Instance.Bullets[keys[0]].Enqueue(BulletManager.Instance.RemoveFind(keys[0], this));
        gameObject.transform.SetParent(ObjectPoolController.Instance.pool.transform);
    }

    public void Depool() { }

    #endregion

    private void LogWarning(string msg) => Debug.LogWarning("[Bullet] " + msg);
}

public class BulletDataContainer
{
    public IMoveable moveable;
    public IgnoreLayerEnum ignoredLayer;
    public float speed;
    public float angle;
    public float dmg;
    public BulletTypeEnum bulletType;

    public BulletDataContainer(BulletTypeEnum bulletType, float speed, float dmg)
    {
        this.bulletType = bulletType;
        this.moveable = new MoveableBulletB();
        this.ignoredLayer = IgnoreLayerEnum.None;
        this.speed = speed;
        this.angle = default;
        this.dmg = dmg;
    }
}
