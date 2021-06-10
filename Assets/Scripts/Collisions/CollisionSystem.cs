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

    public Queue<Bullet> bulletsHit { get; private set; }

    /**********************ACTIONS*************************/

    public bool DistanceCheck(Vector2 pos, Vector2 target, float rad) => Vector2.Distance(pos, target) <= rad;

    //// When the bullet hit the target, it creates an off-sync with the update of the array => spacing the bullets
    private void UpdateCollisionSystem(Dictionary<string, HashSet<Bullet>> bulletsDict, Dictionary<string, HashSet<Unit>> unitsDict, PlayerController player)
    {
        foreach (var b in bulletsDict.Keys.SelectMany(key => bulletsDict[key]))
        {
            if (b.ignoredLayer == IgnoreLayerEnum.Player)
            {
                foreach (var u in unitsDict.Keys.SelectMany(key => unitsDict[key]).Where(u => DistanceCheck(b.transform.position, u.transform.position, u.rad)))
                {
                    u.TakeDamage(b.dmg);
                    bulletsHit.Enqueue(b);
                }
            }
            else
            {
                if (DistanceCheck(b.transform.position, player.transform.position, player.rad))
                {
                    player.TakeDamage(b.dmg);
                    bulletsHit.Enqueue(b);
                }
            }
        }
    }


    private void BatchUpdate(Dictionary<string, HashSet<Bullet>> bulletsDict, Dictionary<string, HashSet<Unit>> unitsDict, PlayerController player)
    {
        if (Time.time - Last > Globals.fps)
        {
            UpdateCollisionSystem(bulletsDict, unitsDict, player);
            Last = Time.time;
        }
    }

    public IEnumerator EmptyBulletColliderQueue()
    {
        while (true)
        {
            if (bulletsHit.Count > 0)
            {
                bulletsHit.Dequeue().Pool();
            }
            yield return null;
        }
    }

    /**********************FLOW****************************/

    public void PreIntilizationMethod()
    {
        Last = default;
        bulletsHit = new Queue<Bullet>();
    }

    public void InitializationMethod() { }

    //// Think of a way to avoir coupling ==> BulletManager ==> UnitManager ==> PlayerController
    public void UpdateMethod() => BatchUpdate(BulletManager.Instance.BulletsDict, UnitManager.Instance.UnitsDict, PlayerController.Instance);
}
