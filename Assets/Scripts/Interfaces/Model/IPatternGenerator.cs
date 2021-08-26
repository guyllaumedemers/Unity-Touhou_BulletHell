using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPatternGenerator
{
    public abstract IProduct Create(string type, Transform parent, Vector2 pos);
    public abstract IProduct[,] Fill(string type, Transform parent, Vector2 pos, int indexI, int indexJ);
    public abstract void UpdateBulletPattern(int indexI, int indexJ);
}
