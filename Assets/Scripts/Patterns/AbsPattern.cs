using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbsPattern : IPatternGenerator
{
    [Header("Pattern Shaper Values")]
    public int nbArr;                   // This parameter sets the amount of total bullet-spawning arrays.
    public int nbPerArr;                // This parameter sets the amount of bullets within each array.
    // Spread
    public float spreadArr;             // This parameter sets the spread between individual bullet arrays. (in degrees)
    public float spreadInArr;           // This parameter sets the spread within the bullet arrays, more specifically, it sets the spread between the first
                                        // and the last bullet of each array. (in degrees)
    public float startAngle;            // This parameters sets the starting angle. (in degrees)
    // Rotation
    public float spinRate;              // This parameters sets the rate at which the bullet arrays will rotate around their origin.
    public float spinMod;               // This parameters sets the modifier with which the Spin Rate will be modified.
    public bool invertSpin;             // Invert Spin is technically a boolean, 1 = The spin rate will invert once the spin rate hits the Max Spin Rate. 0 = Nothing happens.
    public float rof;                   // This parameter sets the rate at which bullets are fired.

    public abstract void UpdateBulletPattern(Transform transform, float speed, float dir);

    //public IEnumerator Shoot(string type, Transform parent, Vector2 pos)
    //{
    //    while (BulletManager.Instance.BulletsDict[type].Count < (nbPerArr * nbArr))
    //    {
    //        FactoryManager.Instance.FactoryMethod<Bullet>(type, parent, pos);
    //        yield return new WaitForSeconds(1 / rof);
    //    }
    //}
}
