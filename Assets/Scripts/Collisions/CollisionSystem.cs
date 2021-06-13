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

    /**********************ACTIONS*************************/

    public bool DistanceCheck(Vector2 pos, Vector2 target, float rad) => Vector2.Distance(pos, target) <= rad;

    //// O(N^2) => R-tree might be the better solution for the lookup
    private void UpdateCollisionSystem(Dictionary<string, HashSet<Bullet>> bulletsDict, Dictionary<string, HashSet<Unit>> unitsDict)
    {

    }

    /**********************FLOW****************************/

    public void PreIntilizationMethod() { }

    public void InitializationMethod() { }

    public void UpdateMethod() => UpdateCollisionSystem(BulletManager.Instance.BulletsDict, UnitManager.Instance.UnitsDict);
}
