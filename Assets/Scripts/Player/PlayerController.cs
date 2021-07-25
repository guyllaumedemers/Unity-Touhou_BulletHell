using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : SingletonMono<PlayerController>, IFlow, IDamageable
{
    private PlayerController() { }
    private Coroutine fireCoroutine;
    private Animator animator;
    private SpriteRenderer sprRen;
    private Transform orbParent;
    private readonly BulletTypeEnum patternFilter = BulletTypeEnum.Missile | BulletTypeEnum.Card;
    private bool collapse = true;
    public UnitDataContainer unitData;
    PlayerInputActions inputs;

    #region public functions

    public void TakeDamage(float dmg) => unitData.health -= dmg;

    #endregion

    #region private functions

    private void Shoot(Transform firingPos)
    {
        unitData.pattern.Fill(unitData.activeBullet, BulletManager.Instance.bulletParent.transform, firingPos.position, 0, 0);
        unitData.pattern.UpdateBulletPattern(default, default);
        foreach (IProduct b in (unitData.pattern as AbsPattern).bullets.Cast<IProduct>()) b.SetIgnoredLayer(IgnoreLayerEnum.Player);
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
            if (Time.time - last > 1 / (unitData.pattern as AbsPattern).rof)
            {
                if (unitData.activeBullet != Globals.missile) Shoot(transform);
                else foreach (Transform orb in orbParent) Shoot(orb);
                last = Time.time;
            }
            yield return null;
        }
    }

    private void Movement()
    {
        Vector2 movement = inputs.Player.Move.ReadValue<Vector2>();
        transform.position += new Vector3(movement.x, movement.y, 0) * unitData.speed * Time.deltaTime;
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
            unitData.activeBullet = unitData.bullets.SwapBulletType(unitData.bulletType);
            unitData.pattern = unitData.bullets.SwapPattern((BulletTypeEnum)System.Enum.Parse(typeof(BulletTypeEnum), unitData.activeBullet));
            foreach (Transform spr in orbParent) StartCoroutine(Utilities.Fade(spr.GetComponent<SpriteRenderer>(), unitData.activeBullet));
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

    #endregion

    #region Unity functions

    private void OnEnable() => inputs.Enable();

    private void OnDisable() => inputs.Disable();

    public void PreIntilizationMethod()
    {
        inputs = new PlayerInputActions();                      // Instanciate a new PlayerInputActions
        inputs.Player.Fire.started += ctx => StartFiring();     // Register the rapid fire for a mouse press
        inputs.Player.Fire.canceled += ctx => StopFiring();     // Stop the coroutine from firing
        unitData = new UnitDataContainer(Globals.hitbox, 100.0f, 5.0f, null, new Queue<string>(), null);
        animator = GetComponent<Animator>();
        sprRen = GetComponent<SpriteRenderer>();
        orbParent = transform.GetChild(0).GetComponent<Transform>();
        OrbRotation.Instance.PreIntilizationMethod();
    }

    public void InitializationMethod()
    {
        foreach (var obj in FactoryManager.Instance.FactoryBullets.Where(x => EnumFiltering.EnumToString(patternFilter).Any(w => w.Equals(x.name))))
        {
            unitData.bulletType.Enqueue(obj.name);
        }
        unitData.activeBullet = unitData.bullets.SwapBulletType(unitData.bulletType);                                                              // initialize the active bullet type string    
        unitData.pattern = unitData.bullets.SwapPattern((BulletTypeEnum)System.Enum.Parse(typeof(BulletTypeEnum), unitData.activeBullet));         // initialize the pattern with the active bullet type
    }

    public void UpdateMethod()
    {
        SwapBulletType();
        ToggleOrb();
        Movement();
        unitData.animation.Animate(animator, sprRen, inputs.Player.Move.ReadValue<Vector2>());
        OrbRotation.Instance.UpdateMethod();
    }

    #endregion
}
