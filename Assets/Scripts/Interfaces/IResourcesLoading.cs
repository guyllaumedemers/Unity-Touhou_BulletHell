using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IResourcesLoading
{
    public abstract GameObject GetPrefab(GameObject[] arr, string type);
    public abstract GameObject[] ResourcesLoading(string path);
}
