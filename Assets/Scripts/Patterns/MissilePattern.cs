using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissilePattern : AbsPattern
{
    public MissilePattern(Transform transform, Transform parent)
    {
        // Set values for this pattern => subject to change
        this.nbArr = 2;
        this.nbPerArr = 1;
        this.xOffset = 0.5f;
        this.yOffset = 1.0f;
        this.spreadArr = 0.0f;
        this.spreadInArr = 2.0f;
        this.startAngle = 90.0f;
        this.spinRate = 0.0f;
        this.spinMod = 0.0f;
        this.invertSpin = false;
        this.rof = 15.0f;
        // Initialize all bullets from the pattern which are then added to the bulletManager so he can update all bullets
        this.bullets = new Bullet[nbArr, nbPerArr];
        this.bullets = Fill("Missile", parent, transform.position, 0, 0);
    }

    // Fill the Bullet array with different position instances of each array
    // so the bullets all start at the center of the transform position of each array
    public override IFactory[,] Fill(string type, Transform transform, Vector2 pos, int indexI, int indexJ)
    {
        if (indexI >= nbArr - 1) return bullets;
        if (indexJ > nbPerArr - 1)
        {
            ++indexI;
            indexJ = 0;
        }
        bullets[indexI, indexJ] = Create(type, transform, pos);
        (bullets[indexI, indexJ] as Bullet).transform.position = Align((bullets[indexI, indexJ] as Bullet).transform.position, indexI);
        return Fill(type, transform, pos, indexI, ++indexJ);
    }

    private Vector2 Align(Vector2 pos, int indexI) => indexI < nbArr / 2 ? new Vector2(pos.x - xOffset, pos.y) : new Vector2(pos.x + xOffset, pos.y);

    public override void UpdateBulletPattern()
    {
        for (int i = 0; i < bullets.GetLength(0); i++)
        {
            for (int j = 0; j < bullets.GetLength(1); j++)
            {
                bullets[i, j].Shoot(startAngle);
            }
        }
    }
}
