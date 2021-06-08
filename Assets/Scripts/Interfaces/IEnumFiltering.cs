using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnumFiltering
{
    public abstract BulletTypeEnum[] Filter(BulletTypeEnum flags);
    public abstract string[] EnumToString(BulletTypeEnum flags);
}
