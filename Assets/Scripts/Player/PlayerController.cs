using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    PlayerInputActions inputs;
    Queue<string> bulletType;
    string activeBullet;
    public float Hitbox { get; private set; }
    const float speed = 5.0f;

    private void Awake()
    {
        inputs = new PlayerInputActions();                  // Instanciate a new PlayerInputActions
        inputs.Player.Fire.started += ctx => Shoot();       // Register to UnityEvent
        Hitbox = 2.0f;                                      // Property for the hitbox radius
                                                            // Initialize Queue with prefabs name for player bullet types
        GameObject[] goTypes = FactoryManager.Instance.FactoryBullets.Where(x => x.GetComponent<PlayerBullet>() != null).ToArray();
        foreach (var obj in goTypes) bulletType.Enqueue(obj.name);
    }

    private void Update()
    {
        if (Keyboard.current.tabKey.wasPressedThisFrame) SwapBulletType();
        Movement();
    }

    /**********************ACTIONS**************************/

    private void Shoot()
    {
        IFactory bullet = FactoryManager.Instance.FactoryMethod<Bullet>(activeBullet, transform, transform.position);
        bullet.Shoot();
        Debug.Log("Fire");
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

    private void OnEnable()
    {
        inputs.Enable();
    }

    private void OnDisable()
    {
        inputs.Disable();
    }
}
