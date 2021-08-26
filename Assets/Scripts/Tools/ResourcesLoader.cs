using System.Linq;
using UnityEngine;

public static class ResourcesLoader
{
    public static GameObject GetPrefab(GameObject[] arr, string type) => arr.FirstOrDefault(go => go.name.Equals(type));
    public static GameObject[] ResourcesLoading(string path) => Utilities.FindResources<GameObject>(path);
}
