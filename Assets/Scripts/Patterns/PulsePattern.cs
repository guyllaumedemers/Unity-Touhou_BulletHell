using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulsePattern : AbsPattern
{
    public PulsePattern(Transform transform, Transform parent)
    {
        // Set values for this pattern => subject to change
        this.nbArr = 8;
        this.nbPerArr = 1;
        this.xOffset = default;
        this.yOffset = default;
        this.spreadArr = default;
        this.spreadInArr = default;
        this.startAngle = default;
        this.spinRate = default;
        this.spinMod = default;
        this.invertSpin = false;
        this.rof = 15.0f;
        // Initialize all bullets from the pattern which are then added to the bulletManager so he can update all bullets
        this.bullets = new Bullet[nbArr, nbPerArr];
        this.bullets = Fill("Circle", parent, transform.position, 0, 0);
    }

    public override void UpdateBulletPattern(int indexI, int indexJ)
    {
        throw new System.NotImplementedException();
    }
}
