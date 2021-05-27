using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPattern : AbsPattern
{
    public CardPattern(Transform transform, Transform parent)
    {
        // Set values for this pattern => subject to change
        this.nbArr = 3;
        this.nbPerArr = 1;
        this.xOffset = 0.5f;
        this.yOffset = 1.0f;
        this.spreadArr = 45.0f;
        this.spreadInArr = 2.0f;
        this.startAngle = 0.0f;
        this.spinRate = 0.0f;
        this.spinMod = 0.0f;
        this.invertSpin = false;
        this.rof = 15.0f;
        // Initialize all bullets from the pattern which are then added to the bulletManager so he can update all bullets
        this.bullets = new Bullet[nbArr, nbPerArr];
    }

    public override void UpdateBulletPattern(int indexI, int indexJ)
    {
        if (indexI >= nbArr - 1) return;
        if (indexJ > nbPerArr - 1)
        {
            ++indexI;
            indexJ = 0;
        }
        bullets[indexI, indexJ].Shoot((startAngle > spreadArr * nbArr - 1) ? startAngle = spreadArr : startAngle += spreadArr);
        UpdateBulletPattern(indexI, ++indexJ);
    }
}
