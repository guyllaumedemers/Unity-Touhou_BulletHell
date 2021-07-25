using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : SingletonMono<PlayerController>, IFlow, IDamageable
{
    private IPatternGenerator pattern;
    private readonly IAnimate animationBehaviour = new UnitAnimationBehaviour();
    private readonly ISwappable bullets = new SwappablePatternBehaviour();
    Coroutine fireCoroutine;
    // Player values
    public float rad { get; private set; }
    private float health;
    private float speed;
    private string activeBullet;
    private bool collapse = true;
    PlayerInputActions inputs;
    Queue<string> bulletType;
    private PlayerController() { }
    private Animator animator;
    private SpriteRenderer sprRen;
    private Transform orbParent;

    #region Bullet Filtering

    private readonly BulletTypeEnum patternFilter = BulletTypeEnum.Missile | BulletTypeEnum.Card;

    #endregion

    #region Player Controller Functions

    private void Shoot(Transform firingPos)
    {
        pattern.Fill(activeBullet, BulletManager.Instance.bulletParent.transform, firingPos.position, 0, 0);
        pattern.UpdateBulletPattern(default, default);
        foreach (IProduct b in (pattern as AbsPattern).bullets.Cast<IProduct>()) b.SetIgnoredLayer(IgnoreLayerEnum.Player);
    }

    private void StartFiring() => fireCoroutine = StartCoroutine(RapidFire());

    private void StopFiring()
    {
        if (fireCoroutine != null) StopCoroutine(fireCoroutine);
    }

    private IEnumerator RapidFire()
    {
        float last = Time.time;
        while (true)
        {
            if (Time.time - last > 1 / (pattern as AbsPattern).rof)
            {
                if (activeBullet != Globals.missile) Shoot(transform);
                else foreach (Transform orb in orbParent) Shoot(orb);
                last = Time.time;
            }
            yield return null;
        }
    }

    private void Movement()
    {
        Vector2 movement = inputs.Player.Move.ReadValue<Vector2>();
        transform.position += new Vector3(movement.x, movement.y, 0) * speed * Time.deltaTime;
        transform.position = Wrap(transform.position);
    }

    private Vector2 Wrap(Vector2 pos)
    {
        var cam = Camera.main;
        var viewport = cam.WorldToViewportPoint(transform.position);
        if (viewport.x < 0 || viewport.x > 1) pos.x = -pos.x;
        if (viewport.y < 0 || viewport.y > 1) pos.y = -pos.y;
        return pos;
    }

    private void SwapBulletType()
    {
        if (Keyboard.current.tabKey.wasPressedThisFrame)
        {
            activeBullet = bullets.SwapBulletType(bulletType);
            pattern = bullets.SwapPattern((BulletTypeEnum)System.Enum.Parse(typeof(BulletTypeEnum), activeBullet));
            foreach (Transform spr in orbParent) StartCoroutine(Utilities.Fade(spr.GetComponent<SpriteRenderer>(), activeBullet));
        }
    }

    private void ToggleOrb()
    {
        if (Keyboard.current.shiftKey.wasPressedThisFrame)
        {
            collapse = !collapse;
            OrbRotation.Instance.ExpandAndCollapse(transform.position, collapse);
        }
    }

    public void TakeDamage(float dmg) => this.health -= dmg;

    #endregion

    #region Input System Functions

    private void OnEnable() => inputs.Enable();

    private void OnDisable() => inputs.Disable();

    #endregion

    #region Unity Functions

    public void PreIntilizationMethod()
    {
        inputs = new PlayerInputActions();                      // Instanciate a new PlayerInputActions
        inputs.Player.Fire.started += ctx => StartFiring();     // Register the rapid fire for a mouse press
        inputs.Player.Fire.canceled += ctx => StopFiring();     // Stop the coroutine from firing
        bulletType = new Queue<string>();
        health = 10.0f;
        speed = 5.0f;
        rad = Globals.hitbox;
        animator = GetComponent<Animator>();
        sprRen = GetComponent<SpriteRenderer>();
        orbParent = transform.GetChild(0).GetComponent<Transform>();
        OrbRotation.Instance.PreIntilizationMethod();
    }

    public void InitializationMethod()
    {
        foreach (var obj in FactoryManager.Instance.FactoryBullets.Where(x => EnumFiltering.EnumToString(patternFilter).Any(w => w.Equals(x.name))))
        {
            bulletType.Enqueue(obj.name);
        }
        activeBullet = bullets.SwapBulletType(bulletType);                                                              // initialize the active bullet type string    
        pattern = bullets.SwapPattern((BulletTypeEnum)System.Enum.Parse(typeof(BulletTypeEnum), activeBullet));         // initialize the pattern with the active bullet type
    }

    public void UpdateMethod()
    {
        SwapBulletType();
        ToggleOrb();
        Movement();
        animationBehaviour.Animate(animator, sprRen, inputs.Player.Move.ReadValue<Vector2>());
        OrbRotation.Instance.UpdateMethod();
    }

    #endregion
}
