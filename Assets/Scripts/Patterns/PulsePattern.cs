using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulsePattern : AbsPattern
{
    public PulsePattern()
    {
        this.nbArr = 8;
        this.nbPerArr = 1;
        this.spreadArr = default;
        this.rof = 15.0f;
        this.bullets = new Bullet[nbArr, nbPerArr];
    }

    public override void UpdateBulletPattern(int indexI, int indexJ)
    {
        throw new System.NotImplementedException();
    }
}
