using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour, IDamageable
{
    public float health;
    public float speed;
    public IPatternGenerator pattern;
    public string activeBullet;
    public Coroutine fireCoroutine;

    /**********************ACTIONS**************************/

    public void UpdateUnit()
    {
        // Update unit position following trajectory
    }

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
