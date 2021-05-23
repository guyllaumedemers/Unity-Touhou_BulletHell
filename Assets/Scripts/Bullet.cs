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

    private void Awake()
    {
        InitializeMethod();
        //// I am trying to set an ID for a new Bullet in order to batch update in the BulletManager
        //// Problem : I have multiple bullet Types => for player and Enemies
        //// Setting an ID in a linear way means having a Bullets of a different type gettings assigned a high value ID
        //// which invole not being updated at the current state of the batching update
        ////
        //// Why is this a problem?
        //// If a player shoot multiple regular bullets => they get updated linearly by the batching system
        //// What if the player switch bullet mid-way and start shooting cards
        //// cards would be assigned the next ID after the last bullet ID for the regular type
        //// meaning they might be outside the range of the current batching range
    }

    //// Bullet Update position will be different depending on the pattern => Boss, Mobs, etc...
    //// Dont forget to think about the direction in which they travel
    public virtual void UpdateBulletPosition()
    {
        transform.position += new Vector3(pos.x, pos.y, 0) * speed * Time.deltaTime;
    }

    //// BulletType are going to define their own Shoot function
    public abstract void Shoot();

    //// Bullets should be checking for the distance between it and the target
    public bool DistanceCheck(Vector2 pos, Vector2 target)
    {
        return Vector2.Distance(pos, target) <= rad;
    }

    private void OnBecameInvisible()
    {
        Pool();
    }

    public void Pool()
    {
        string[] keys = gameObject.name.Split('(');
        ObjectPool.Bullets[keys[0]].Enqueue(BulletManager.Instance.BulletsDict[keys[0]].Dequeue());
        gameObject.SetActive(false);
    }

    public void Depool()
    {
        gameObject.SetActive(true);
    }

    public void ResetBullet(Vector2 newPos)
    {
        pos = newPos;
    }

    private void InitializeMethod() => pos = new Vector2();
}
