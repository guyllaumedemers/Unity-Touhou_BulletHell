using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : AbsUnit
{
    private Coroutine firecoroutine = null;
    private Animator animator;
    private SpriteRenderer sprRen;
    private readonly BulletTypeEnum patternFilter = BulletTypeEnum.Missile | BulletTypeEnum.Card;
    public UnitDataContainer unitData { get; private set; }
    PlayerInputActions inputs;
    Camera gamecamera;

    #region interfaces
    public override void StartFiring(ref Coroutine routine)
    {
        if (routine != null)
        {
            StopFiring(ref routine);
        }
        routine = StartCoroutine(RapidFire());
    }

    public override void StopFiring(ref Coroutine routine)
    {
        if (routine != null) StopCoroutine(routine);
    }

    public override void Shoot(Transform parent, Transform pos)
    {
        unitData.pattern.Fill(unitData.activeBullet, parent, pos.position, 0, 0);
        unitData.pattern.UpdateBulletPattern(default, default);
        foreach (Bullet b in (unitData.pattern as AbsPattern).bullets) b.SetIgnoredLayer(IgnoreLayerEnum.Player);
    }

    public override void Move()
    {
        Vector2 movement = inputs.Player.Move.ReadValue<Vector2>();
        transform.position += new Vector3(movement.x, movement.y, 0) * unitData.speed * Time.deltaTime;
        transform.position = Wrap(gamecamera, transform.position);
    }

    public override void TakeDamage(float dmg)
    {
        throw new System.NotImplementedException();
    }
    #endregion

    #region private functions
    private IEnumerator RapidFire()
    {
        float last = Time.time;
        while (true)
        {
            if (Time.time - last > 1 / (unitData.pattern as AbsPattern).rof)
            {
                Shoot(BulletManager.Instance.bulletParent.transform, transform);
                last = Time.time;
            }
            yield return null;
        }
    }
    private Vector3 Wrap(Camera cam, Vector2 pos)
    {
        var viewport = cam.WorldToViewportPoint(transform.position);
        if (viewport.x < 0 || viewport.x > 1) pos.x = -pos.x;
        if (viewport.y < 0 || viewport.y > 1) pos.y = -pos.y;
        return new Vector3(pos.x, pos.y, 10);
    }

    private void SwapBulletType()
    {
        if (Keyboard.current.tabKey.wasPressedThisFrame)
        {
            unitData.activeBullet = unitData.bullets.SwapBulletType(unitData.bulletTypeList);
            unitData.pattern = unitData.bullets.SwapPattern((BulletTypeEnum)System.Enum.Parse(typeof(BulletTypeEnum), unitData.activeBullet));
        }
    }
    //private void OnEnable() => inputs?.Enable();
    private void OnDisable() => inputs?.Disable();
    #endregion


    public void PreIntilizationPlayerController()
    {
        inputs = new PlayerInputActions();                                          // Instanciate a new PlayerInputActions
        inputs.Player.Fire.started += ctx => StartFiring(ref firecoroutine);        // Register the rapid fire for a mouse press
        inputs.Player.Fire.canceled += ctx => StopFiring(ref firecoroutine);        // Stop the coroutine from firing
        unitData = DatabaseHandler.RetrieveTableEntries<UnitDataContainer>(SQLTableEnum.UnitData.ToString()).Where(x => x.unitType.ToString().Equals(UnitTypeEnum.Player.ToString())).FirstOrDefault();
        animator = GetComponent<Animator>();
        sprRen = GetComponent<SpriteRenderer>();
        gamecamera = FindObjectsOfType<Camera>().Where(x => x.gameObject.tag.Equals(Globals.gameview)).FirstOrDefault();
        inputs.Enable();                                                            // Enable cannot be called in the OnEnable function because of unity order of execution
    }

    public void InitializationPlayerController()
    {
        foreach (var obj in FactoryManager.Instance.FactoryBullets.Where(x => EnumFiltering.EnumToString(patternFilter).Any(w => w.Equals(x.name))))
        {
            unitData.bulletTypeList.Enqueue(obj.name);
        }
        unitData.activeBullet = unitData.bullets.SwapBulletType(unitData.bulletTypeList);                                                              // initialize the active bullet type string    
        unitData.pattern = unitData.bullets.SwapPattern((BulletTypeEnum)System.Enum.Parse(typeof(BulletTypeEnum), unitData.activeBullet));         // initialize the pattern with the active bullet type
    }

    public void UpdatePlayerController()
    {
        SwapBulletType();
        Move();
        unitData.animation.Animate(animator, sprRen, inputs.Player.Move.ReadValue<Vector2>());
    }
}
