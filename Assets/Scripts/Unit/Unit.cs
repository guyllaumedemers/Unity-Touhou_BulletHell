using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour, IDamageable
{
    public float health;
    public float speed;
    public IPatternGenerator pattern;

    /**********************ACTIONS**************************/

    public void UpdateUnit()
    {
        // Update the unit position
    }

    public IEnumerator Play()
    {
        while (true)
        {
            yield return new WaitForSeconds(1/ (pattern as AbsPattern).rof);
        }
    }

    public void TakeDamage(int dmg) => health -= dmg;
}
