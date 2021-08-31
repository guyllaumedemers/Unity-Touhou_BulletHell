using UnityEngine;

public interface IPatternGenerator
{
    public abstract Bullet Create(string type, Transform parent, Vector2 pos);
    public abstract Bullet[,] Fill(string type, Transform parent, Vector2 pos, int indexI, int indexJ);
    public abstract void UpdateBulletPattern(int indexI, int indexJ);
}
