using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPatternGenerator
{
    public abstract void Play<T>(string type, Transform parent, Vector2 pos) where T : class;
}
