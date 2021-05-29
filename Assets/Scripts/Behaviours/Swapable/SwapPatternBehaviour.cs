using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapPatternBehaviour : ISwap
{
    public void SwapBulletType(Queue<string> bulletType, string activeBullet)
    {
        activeBullet = bulletType.Dequeue();
        bulletType.Enqueue(activeBullet);
    }

    public IPatternGenerator SwapPattern(PatternEnum pattern) => pattern switch                     // Single switch expression that handle all Pattern instanciation
    {                                                                                               // Patterns are filtered inside the class calling it thru custom enum
        PatternEnum.Missile => new MissilePattern(),
        PatternEnum.Card => new CardPattern(),
        PatternEnum.Circle => new PulsePattern(),
        PatternEnum.Star => default,                                                                // need to create pattern for the star bullet type
        _ => default,
    };
}
