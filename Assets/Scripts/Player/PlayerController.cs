using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    PlayerInputActions inputs;
    public float Hitbox { get; private set; }
    const float speed = 5.0f;

    private void Awake()
    {
        inputs = new PlayerInputActions();                  // Instanciate a new PlayerInputActions
        inputs.Player.Fire.started += ctx => Shoot();       // Register to UnityEvent
        Hitbox = 2.0f;                                      // Property for the hitbox radius
    }

    private void Update()
    {
        Movement();
    }

    /**********************ACTIONS**************************/

    private void Shoot()
    {
        //// Instanciate Bullets
        IFactory bullet = FactoryManager.Instance.FactoryMethod<Bullet>("Insert Type");
        bullet.Shoot();
        Debug.Log("Fire");
    }

    private void Movement()
    {
        Vector2 movement = inputs.Player.Move.ReadValue<Vector2>();
        transform.position += new Vector3(movement.x, movement.y, 0) * speed * Time.deltaTime;
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
