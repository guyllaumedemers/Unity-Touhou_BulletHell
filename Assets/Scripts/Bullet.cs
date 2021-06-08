using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public abstract class Bullet : MonoBehaviour, IProduct, IPoolable
{
    IMoveable moveable = new MoveableBulletBehaviour();
    public IgnoreLayerEnum ignoredLayer;
    // bullet values
    private const float speed = 5;
    public float angle;
    public float rad;

    /**********************ACTIONS**************************/

    private void Awake() => SetRadian(2.0f);

    //// Bullet Update position will be different depending on the pattern => Boss, Mobs, etc...
    //// Dont forget to think about the direction in which they travel
    public virtual void UpdateBulletPosition() => transform.position = moveable.Move(transform.position, angle, speed);

    public void SetIgnoredLayer(IgnoreLayerEnum layer) => ignoredLayer = layer;

    public void SetAngle(float angle) => this.angle = angle;

    public void SetRadian(float rad) => this.rad = rad;

    public void ResetTransformPos(Vector2 newPos) => transform.position = newPos;

    private void OnBecameInvisible() => Pool();


    /**********************POOL****************************/

    public void Pool()
    {
        string[] keys = gameObject.name.Split('(');
        ObjectPool.LastUpdate[keys[0]] = Time.time;
        ObjectPool.Bullets[keys[0]].Enqueue(BulletManager.Instance.RemoveFind(keys[0], this) as Bullet);
        gameObject.SetActive(false);
    }

    public void Depool() => gameObject.SetActive(true);
}
