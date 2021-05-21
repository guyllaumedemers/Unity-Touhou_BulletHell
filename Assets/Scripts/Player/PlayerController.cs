using UnityEngine;

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
        IFactory bullet = FactoryManager.Instance.FactoryMethod<Bullet>("Insert Type", transform, transform.position);
        bullet.Shoot();
        Debug.Log("Fire");
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
