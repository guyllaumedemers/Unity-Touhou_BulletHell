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
    public float Hitbox { get; private set; }

    /**********************ACTIONS**************************/

    private void Shoot()
    {
        IFactory bullet = FactoryManager.Instance.FactoryMethod<Bullet>(activeBullet, transform, transform.position);
        (bullet as Bullet).ResetBullet(transform.position);
        bullet.Shoot();
    }

    private void SwapBulletType()
    {
        activeBullet = bulletType.Dequeue();
        bulletType.Enqueue(activeBullet);
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

    public void PreIntilizationMethod()
    {
        inputs = new PlayerInputActions();                  // Instanciate a new PlayerInputActions
        inputs.Player.Fire.started += ctx => Shoot();       // Register to UnityEvent
        Hitbox = 2.0f;                                      // Property for the hitbox radius
        bulletType = new Queue<string>();
    }

    public void InitializationMethod()
    {
        GameObject[] goTypes = FactoryManager.Instance.FactoryBullets.Where(x => x.GetComponent<PlayerBullet>()).ToArray();
        foreach (var obj in goTypes) bulletType.Enqueue(obj.name);
        SwapBulletType();                                   // initialize the active bullet type string
                                                            // active bullet getting assigned => tested => working
        //Debug.Log(string.Format("Active bullet {0}", activeBullet));      
    }

    public void UpdateMethod()
    {
        if (Keyboard.current.tabKey.wasPressedThisFrame) SwapBulletType();
        Movement();
    }
}
