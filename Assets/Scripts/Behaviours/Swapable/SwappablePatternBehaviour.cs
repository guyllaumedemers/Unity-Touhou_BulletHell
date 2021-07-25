using UnityEngine;
using System.Collections.Generic;

public class SwappablePatternBehaviour : ISwappable
{
    public string SwapBulletType(Queue<string> bulletType)
    {
        if (bulletType.Count < 1)
        {
            LogWarning("Bullet Type Array is Empty");
            return null;
        }
        string bulletTypeRemoved = bulletType.Dequeue();
        bulletType.Enqueue(bulletTypeRemoved);
        return bulletTypeRemoved;
    }

    public IPatternGenerator SwapPattern(BulletTypeEnum pattern)
    {
        return pattern switch
        {
            BulletTypeEnum.Missile => new MissilePattern(),
            BulletTypeEnum.Card => new CardPattern(),
            BulletTypeEnum.Circle => new PulsePattern(),
            BulletTypeEnum.Star => default,
            BulletTypeEnum.None => throw new System.InvalidOperationException(),
            _ => throw new System.NotImplementedException()
        };
    }

    private void LogWarning(string msg) => Debug.LogWarning("[Swappable Behaviour] " + msg);
}
