using System.Linq;
using UnityEngine;

public class ResourcesLoadingBehaviour : IResourcesLoading
{
    public GameObject GetPrefab(GameObject[] arr, string type) => arr.FirstOrDefault(go => go.name.Equals(type));
    public GameObject[] ResourcesLoading(string path) => Utilities.FindResources<GameObject>(path);
}
