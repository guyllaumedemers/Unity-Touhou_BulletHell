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
    private const float speed = 5.0f;
    private string activeBullet;
    private const float rof = 15.0f;
    private Coroutine fireCoroutine;
    public float Hitbox { get; private set; }

    private IPatternGenerator pattern;
    private string[] playerBullets = { "Missile", "Card" };         // Find a cleaner way to filter Player Bullets

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
            if (Time.time - last > 1 / rof)
            {
                Shoot();
                last = Time.time;
            }
            yield return null;
        }
    }

    private void SwapBulletType()
    {
        activeBullet = bulletType.Dequeue();
        bulletType.Enqueue(activeBullet);
    }

    // Not Scalable => Find better way to do this
    private IPatternGenerator SwapPattern(Transform transform, Transform parent, string pattern)
    {
        if (pattern.Equals("Missile")) return new MissilePattern(transform, parent);
        if (pattern.Equals("Card")) return new CardPattern(transform, parent);
        return null;
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
    }

    public void InitializationMethod()
    {
        foreach (var obj in FactoryManager.Instance.FactoryBullets.Where(x => playerBullets.Any(w => w.Equals(x.name)))) bulletType.Enqueue(obj.name);
        SwapBulletType();                                       // initialize the active bullet type string    
        pattern = SwapPattern(transform, null, activeBullet);   // initialize the pattern with the active bullet type
    }

    public void UpdateMethod()
    {
        if (Keyboard.current.tabKey.wasPressedThisFrame)
        {
            SwapBulletType();
            pattern = SwapPattern(transform, null, activeBullet);
        }
        Movement();
    }
}
