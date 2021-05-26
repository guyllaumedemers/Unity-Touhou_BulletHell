using UnityEngine;

public abstract class AbsPattern : IPatternGenerator
{
    [Header("Pattern Shaper Values")]
    public int nbArr;                   // This parameter sets the amount of total bullet-spawning arrays.
    public int nbPerArr;                // This parameter sets the amount of bullets within each array.
    public float xOffset;
    public float yOffset;
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

    public IFactory[,] bullets;

    public IFactory Create(string type, Transform transform, Vector2 pos)
    {
        return FactoryManager.Instance.FactoryMethod<Bullet>(type, transform, pos);
    }

    // Fill the Bullet array with the same position for all instances 
    // so the bullets all start at the center of the transform position
    public virtual IFactory[,] Fill(string type, Transform transform, Vector2 pos, int indexI, int indexJ)
    {
        if (indexI >= nbArr)
            return bullets;
        if (indexJ >= nbPerArr)
        {
            indexJ = 0;
            ++indexI;
        }
        bullets[indexI, indexJ] = Create(type, transform, pos);
        return Fill(type, transform, pos, indexI, ++indexJ);
    }

    public abstract void UpdateBulletPattern();
}
