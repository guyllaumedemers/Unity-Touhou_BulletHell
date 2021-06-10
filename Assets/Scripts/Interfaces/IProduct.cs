using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IProduct
{
    public abstract void SetIgnoredLayer(IgnoreLayerEnum layer);
    public abstract void SetAngle(float angle);
    public abstract void ResetTransformPos(Vector2 newPos);
}
