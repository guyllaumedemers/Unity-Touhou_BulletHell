using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissilePattern : AbsPattern
{
    public MissilePattern()
    {
        this.nbArr = 2;
        this.nbPerArr = 1;
        this.xOffset = 0.5f;
        this.startAngle = 90.0f;
        this.rof = 15.0f;
        this.bullets = new Bullet[nbArr, nbPerArr];
    }

    // Fill the Bullet array with different position instances of each array
    // so the bullets all start at the center of the transform position of each array
    public override IProduct[,] Fill(string type, Transform transform, Vector2 pos, int indexI, int indexJ)
    {
        if (indexI >= nbArr - 1) return bullets;
        if (indexJ > nbPerArr - 1)
        {
            ++indexI;
            indexJ = 0;
        }
        bullets[indexI, indexJ] = Create(type, transform, pos);
        //(bullets[indexI, indexJ] as Bullet).transform.position = Align((bullets[indexI, indexJ] as Bullet).transform.position, indexI);
        return Fill(type, transform, pos, indexI, ++indexJ);
    }

    private Vector2 Align(Vector2 pos, int indexI) => indexI < nbArr / 2 ? new Vector2(pos.x - xOffset, pos.y) : new Vector2(pos.x + xOffset, pos.y);

    public override void UpdateBulletPattern(int indexI, int indexJ)
    {
        if (indexI >= nbArr - 1) return;
        if (indexJ > nbPerArr - 1)
        {
            ++indexI;
            indexJ = 0;
        }
        bullets[indexI, indexJ].SetAngle(startAngle);
        UpdateBulletPattern(indexI, ++indexJ);
    }
}
