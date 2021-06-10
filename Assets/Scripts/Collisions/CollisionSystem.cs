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
    public float Last { get; private set; }

    public HashSet<Bullet> bulletsHit { get; private set; }     // hashset to avoid adding duplicate when doing the distance check

    /**********************ACTIONS*************************/

    public bool DistanceCheck(Vector2 pos, Vector2 target, float rad) => Vector2.Distance(pos, target) <= rad;

    //// O(N^2)
    private void UpdateCollisionSystem(Dictionary<string, HashSet<Bullet>> bulletsDict, Dictionary<string, HashSet<Unit>> unitsDict)
    {
        foreach (var b in bulletsDict.Keys.SelectMany(key => bulletsDict[key]).Where(x => x.ignoredLayer == IgnoreLayerEnum.Player))
        {
            if (bulletsHit.Contains(b)) continue;
            foreach (var u in unitsDict.Keys.SelectMany(key => unitsDict[key]).Where(x => DistanceCheck(b.transform.position, x.transform.position, x.rad)))
            {
                u.TakeDamage(b.dmg);
                bulletsHit.Add(b);
            }
        }

        while (bulletsHit.Count > 0)                                // might affect the main thread
        {
            IProduct bullet = bulletsHit.First();
            bulletsHit.Remove(bullet as Bullet);
            (bullet as Bullet).Pool();
        }
    }

    /**********************FLOW****************************/

    public void PreIntilizationMethod()
    {
        Last = default;
        bulletsHit = new HashSet<Bullet>();
    }

    public void InitializationMethod() { }

    //// Think of a way to avoid coupling ==> BulletManager ==> UnitManager ==> PlayerController
    public void UpdateMethod() => UpdateCollisionSystem(BulletManager.Instance.BulletsDict, UnitManager.Instance.UnitsDict);
}
