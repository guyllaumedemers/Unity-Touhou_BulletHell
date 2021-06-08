using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnumFiltering
{
    public abstract PatternEnum[] Filter(PatternEnum flags);
    public abstract string[] EnumToString(PatternEnum flags);
}
