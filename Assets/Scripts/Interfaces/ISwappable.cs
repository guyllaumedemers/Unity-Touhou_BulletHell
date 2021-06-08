using System.Collections;
using System.Collections.Generic;

public interface ISwappable
{
    public abstract string SwapBulletType(Queue<string> bulletType);
    public abstract IPatternGenerator SwapPattern(PatternEnum pattern);
}
