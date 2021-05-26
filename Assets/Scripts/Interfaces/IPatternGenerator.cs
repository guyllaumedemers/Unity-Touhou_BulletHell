using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPatternGenerator
{
    public abstract IFactory Create(string type, Transform transform, Vector2 pos);
    public abstract IFactory[,] Fill(string type, Transform transform, Vector2 pos, int indexI, int indexJ);
    public abstract void UpdateBulletPattern();
}
