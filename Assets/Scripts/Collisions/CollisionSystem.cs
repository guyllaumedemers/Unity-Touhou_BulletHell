using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CollisionSystem : SingletonMono<CollisionSystem>, IFlow
{
    /*  What I am trying to achieve is :
     *      
     *      I want to be able to do a distance check between a unit AND a bullet that has a flag set to the layer to ignore
     *      
     *  
     */
    private Queue<IProduct> products = new Queue<IProduct>();

    /**********************ACTIONS*************************/

    public bool DistanceCheck(Vector2 pos, Vector2 target, float rad) => Vector2.Distance(pos, target) <= rad;

    //// O(N^2) => R-tree might be the better solution for the lookup
    private void UpdateCollisionSystem(Dictionary<string, HashSet<Bullet>> bulletsDict, Dictionary<string, HashSet<Unit>> unitsDict)
    {
        foreach (var u in unitsDict.Keys.SelectMany(key => unitsDict[key]))
        {
            foreach (var b in bulletsDict.Keys.SelectMany(key => bulletsDict[key]).Where(x => DistanceCheck(u.transform.position, x.transform.position, u.rad)))
            {
                u.TakeDamage(b.dmg);
                products.Enqueue(b);
            }
        }
        while (products.Count > 0) (products.Dequeue() as Bullet).Pool();
    }

    /**********************FLOW****************************/

    public void PreIntilizationMethod() { }

    public void InitializationMethod() { }

    public void UpdateMethod() => UpdateCollisionSystem(BulletManager.Instance.BulletsDict, UnitManager.Instance.UnitsDict);
}
