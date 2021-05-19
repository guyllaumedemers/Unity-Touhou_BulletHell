using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    private Dictionary<string, Queue<Bullet>> bulletsDict;
    private readonly string path = "prefabs/BulletTypes/";

    private void Awake()
    {
        ///// How it is going to work
        ///// Instead of keeping track of BulletType, we are going to put into a single folder all sub-script
        ///// defining a bullet type and Resources.LoadAll inside the dependency in order to retrieve their string name onStart\
        ///// and fill the dictionnary
        bulletsDict = new Dictionary<string, Queue<Bullet>>();
    }

    private void Update()
    {
        ///// BulletManager is going Update Bullets every cycle
        UpdateBullets(bulletsDict);
    }

    private void UpdateBullets(Dictionary<string, Queue<Bullet>> bulletsDict)
    {
        ///// How to we add batching to this?
        foreach (var value in bulletsDict.Keys.SelectMany(key => bulletsDict[key]))
        {
            value.UpdateBulletPosition();
        }
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
