using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFlow
{
    public abstract void PreIntilizationMethod();
    public abstract void InitializationMethod();
    public abstract void UpdateMethod();
}
