using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : SingletonMono<PlayerController>, IFlow
{
    PlayerInputActions inputs;
    Queue<string> bulletType;
    private PlayerController() { }
    Coroutine fireCoroutine;
    // PlayerController values
    private const float speed = 5.0f;
    private string activeBullet;
    public float Hitbox { get; private set; }
    private IPatternGenerator pattern;
    private ISwappable bullets;

    private string[] EnumToString() => System.Enum.GetNames(typeof(PatternEnumPlayer));

    /**********************ACTIONS**************************/

    private void Shoot()
    {
        pattern.Fill(activeBullet, null, transform.position, 0, 0);
        pattern.UpdateBulletPattern(default, default);
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
                Shoot();
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

    /**********************ENABLE**************************/

    private void OnEnable() => inputs.Enable();

    private void OnDisable() => inputs.Disable();

    /**********************FLOW****************************/

    public void PreIntilizationMethod()
    {
        inputs = new PlayerInputActions();                      // Instanciate a new PlayerInputActions
        inputs.Player.Fire.started += ctx => StartFiring();     // Register the rapid fire for a mouse press
        inputs.Player.Fire.canceled += ctx => StopFiring();     // Stop the coroutine from firing
        Hitbox = 2.0f;                                          // Property for the hitbox radius
        bulletType = new Queue<string>();
        bullets = new SwappablePatternBehaviour();
    }

    public void InitializationMethod()
    {
        foreach (var obj in FactoryManager.Instance.FactoryBullets.Where(x => EnumToString().Any(w => w.Equals(x.name)))) bulletType.Enqueue(obj.name);
        bullets.SwapBulletType(bulletType, activeBullet);                                                                        // initialize the active bullet type string    
        pattern = bullets.SwapPattern((PatternEnum)System.Enum.Parse(typeof(PatternEnum), activeBullet));                        // initialize the pattern with the active bullet type
    }

    public void UpdateMethod()
    {
        if (Keyboard.current.tabKey.wasPressedThisFrame)
        {
            bullets.SwapBulletType(bulletType, activeBullet);
            pattern = bullets.SwapPattern((PatternEnum)System.Enum.Parse(typeof(PatternEnum), activeBullet));
        }
        Movement();
    }
}
