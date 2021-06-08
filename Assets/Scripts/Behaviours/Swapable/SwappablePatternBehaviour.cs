using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwappablePatternBehaviour : ISwappable
{
    //// Only depend on preference as I could have done : bulletType.Peek for return instead of returning the one Dequeue
    public string SwapBulletType(Queue<string> bulletType)
    {
        string bulletTypeRemoved = bulletType.Dequeue();
        bulletType.Enqueue(bulletTypeRemoved);
        return bulletTypeRemoved;
    }

    public IPatternGenerator SwapPattern(BulletTypeEnum pattern) => pattern switch                     // Single switch expression that handle all Pattern instanciation
    {                                                                                               // Patterns are filtered inside the class calling it thru custom enum
        BulletTypeEnum.Missile => new MissilePattern(),
        BulletTypeEnum.Card => new CardPattern(),
        BulletTypeEnum.Circle => new PulsePattern(),
        BulletTypeEnum.Star => default,                                                                // need to create pattern for the star bullet type
        _ => default,
    };
}
