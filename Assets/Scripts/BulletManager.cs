using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    private Dictionary<BulletType, Queue<Bullet>> bulletsDict;
    private readonly string path = "prefabs/BulletTypes/";

    private void Awake()
    {
        bulletsDict = new Dictionary<BulletType, Queue<Bullet>>();
    }

    private void Update()
    {

    }

    //// Instanciate Generic Type
    public T InstanciateType<T>(GameObject prefab, Vector2 pos) where T : class
    {
        return Instantiate(prefab, pos, Quaternion.identity, transform).GetComponent<T>();
    }

    /***********************Loading Resources*********************************/
    public T[] FindResources<T>(string path) where T : class
    {
        return Resources.LoadAll(path, typeof(T)).Cast<T>().ToArray();
    }
}
