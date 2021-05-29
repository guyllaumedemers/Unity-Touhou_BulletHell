using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMoveable
{
    public abstract Vector3 Move(Vector3 pos, float angle, float speed);
}
