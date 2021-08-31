using UnityEngine;

public abstract class AbsUnit : MonoBehaviour, IDamageable
{
    public abstract void Shoot(Transform parent, Transform pos);
    public abstract void StartFiring(ref Coroutine routine);
    public abstract void StopFiring(ref Coroutine routine);
    public abstract void Move();

    public abstract void TakeDamage(float dmg); //temp
}
