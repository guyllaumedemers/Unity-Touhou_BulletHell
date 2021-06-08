using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Unit : MonoBehaviour, IDamageable
{
    public IPatternGenerator pattern;
    public IMoveable moveable;
    public readonly IEnumFiltering enumFiltering = new EnumFilteringBehaviour();
    public readonly ISwappable bullets = new SwappablePatternBehaviour();
    public Coroutine fireCoroutine;
    // Unit values
    public float health;
    public float speed;
    public float angle;
    public string activeBullet;
    public Queue<string> bulletType;


    /****************FILTERING BULLETS ENUM******************/

    public PatternEnum patternFilter = PatternEnum.Circle | PatternEnum.Star;

    /**********************ACTIONS**************************/

    public Unit PreInitializeUnit()
    {
        foreach (var obj in FactoryManager.Instance.FactoryBullets.Where(x => enumFiltering.EnumToString(patternFilter).Any(w => w.Equals(x.name)))) bulletType.Enqueue(obj.name);
        activeBullet = bullets.SwapBulletType(bulletType);                                                                  // initialize the active bullet type string    
        pattern = bullets.SwapPattern((PatternEnum)System.Enum.Parse(typeof(PatternEnum), activeBullet));                   // initialize the pattern with the active bullet type
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

    public void TakeDamage(int dmg) => health -= dmg;

    /**********************DISABLE**************************/

    public void OnBecameInvisible() => StopFiring();
}
