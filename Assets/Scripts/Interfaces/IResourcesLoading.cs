using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IResourcesLoading
{
    public abstract void ResourcesLoading(GameObject[] arr, string path);
    public abstract GameObject GetPrefab(GameObject[] arr, string type);
}
