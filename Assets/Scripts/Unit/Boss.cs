using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Unit
{
    public override string[] EnumToString() => System.Enum.GetNames(typeof(PatternEnumBossUnit));
}
