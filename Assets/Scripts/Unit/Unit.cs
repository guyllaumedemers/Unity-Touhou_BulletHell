using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Unit : MonoBehaviour, IDamageable
{
    public IPatternGenerator pattern;
    public IMoveable moveable = new MoveableBossBehaviour(); // Temp => Have to make a generic behaviour for units and Boss => Testing purpose Only
    public readonly IEnumFiltering enumFiltering = new EnumFilteringBehaviour();
    public readonly ISwappable bullets = new SwappablePatternBehaviour();
    public Coroutine fireCoroutine;
    // Unit values
    public float rad { get; private set; }
    public float health;
    public float speed;
    public float angle;
    public string activeBullet;
    public Queue<string> bulletType;

    /**********************ACTIONS**************************/

    // Bullet Type is now pass as arguments so i can parametrize the instanciation of an unit type directly in the function call instead of having values all around and overwriting
    public Unit PreInitializeUnit(BulletTypeEnum bulletT)
    {
        bulletType = new Queue<string>();
        rad = Globals.hitbox;
        foreach (var obj in FactoryManager.Instance.FactoryBullets.Where(x => enumFiltering.EnumToString(bulletT).Any(w => w.Equals(x.name)))) bulletType.Enqueue(obj.name);
        activeBullet = bullets.SwapBulletType(bulletType);                                                                      // initialize the active bullet type string    
        pattern = bullets.SwapPattern((BulletTypeEnum)System.Enum.Parse(typeof(BulletTypeEnum), activeBullet));                 // initialize the pattern with the active bullet type
        return this;
    }

    public void UpdateUnit() => transform.position = moveable.Move(transform.position, angle, speed);

    public IEnumerator Play()
    {
        while (true)
        {
            pattern.Fill(activeBullet, null, transform.position, 0, 0);
            yield return new WaitForSeconds(1 / (pattern as AbsPattern).rof);
        }
    }

    public void StartFiring() => fireCoroutine = StartCoroutine(Play());

    public void StopFiring()
    {
        if (fireCoroutine != null) StopCoroutine(fireCoroutine);
    }

    public void TakeDamage(float dmg) => health -= dmg;

    /**********************DISABLE**************************/

    public void OnBecameInvisible() => StopFiring();
}
