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

    /**********************ACTIONS*************************/

    //// Bullets should be checking for the distance between it and the target
    public bool DistanceCheck(Vector2 pos, Vector2 target, float rad) => Vector2.Distance(pos, target) <= rad;

    private void UpdateCollisionSystem(Dictionary<string, HashSet<Bullet>> bulletsDict, Dictionary<string, HashSet<Unit>> unitsDict, PlayerController player)
    {
        Queue<Bullet> bullets = new Queue<Bullet>();
        foreach (var b in bulletsDict.Keys.SelectMany(key => bulletsDict[key]))
        {
            if (b.ignoredLayer == IgnoreLayerEnum.Player)
            {
                foreach (var u in unitsDict.Keys.SelectMany(key => unitsDict[key]).Where(u => DistanceCheck(b.transform.position, u.transform.position, b.rad)))
                {
                    Debug.Log("Enemy hit");
                    u.TakeDamage(b.dmg);
                    bullets.Enqueue(b);
                }
            }
            else
            {
                if (DistanceCheck(b.transform.position, player.transform.position, b.rad))
                {
                    player.TakeDamage(b.dmg);
                    bullets.Enqueue(b);
                }
            }
        }
        while (bullets.Count > 0) bullets.Dequeue().Pool();
    }

    //// Test if you can batch update the collision update or if it creates weird behaviour like bullet hit not getting register when skipping frames
    private void BatchUpdate(Dictionary<string, HashSet<Bullet>> bulletsDict, Dictionary<string, HashSet<Unit>> unitsDict, PlayerController player)
    {
        if (Time.time - Last > Globals.fps)
        {
            UpdateCollisionSystem(bulletsDict, unitsDict, player);
            Last = Time.time;
        }
    }

    /**********************FLOW****************************/

    public void PreIntilizationMethod() => Last = default;

    public void InitializationMethod() { }

    //// Think of a way to avoir coupling ==> BulletManager ==> UnitManager ==> PlayerController
    public void UpdateMethod() => BatchUpdate(BulletManager.Instance.BulletsDict, UnitManager.Instance.UnitsDict, PlayerController.Instance);
}
