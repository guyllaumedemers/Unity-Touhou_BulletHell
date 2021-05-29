using System.Collections;
using System.Collections.Generic;

public interface ISwap
{
    public abstract void SwapBulletType(Queue<string> bulletType, string activeBullet);
    public abstract IPatternGenerator SwapPattern(PatternEnum pattern);
}
