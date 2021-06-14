using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CollisionSystem : SingletonMono<CollisionSystem>, IFlow
{
    private Queue<IProduct> products = new Queue<IProduct>();

    /**********************ACTIONS*************************/

    public bool DistanceCheck(Vector2 pos, Vector2 target, float rad) => Vector2.Distance(pos, target) <= rad;

    //// O(N^2) => R-tree might be the better solution for the lookup
    private void UpdateCollisionSystem(Dictionary<string, HashSet<Bullet>> bulletsDict, Dictionary<string, HashSet<Unit>> unitsDict, PlayerController player)
    {
        foreach (var b in bulletsDict.Keys.SelectMany(key => bulletsDict[key]))
        {
            if (b.ignoredLayer != IgnoreLayerEnum.Player)
            {
                // do distance check with the player
                if (DistanceCheck(b.transform.position, player.transform.position, player.rad))
                {
                    player.TakeDamage(b.dmg);
                    products.Enqueue(b);
                }
            }
            else
            {
                // do distance check with the units
                foreach (var u in unitsDict.Keys.SelectMany(key => unitsDict[key]).Where(x =>
                DistanceCheck(b.transform.position, x.transform.position, x.rad)))
                {
                    u.TakeDamage(b.dmg);
                    products.Enqueue(b);
                }
            }
        }
        while (products.Count > 0) (products.Dequeue() as Bullet).Pool();
    }

    /**********************FLOW****************************/

    public void PreIntilizationMethod() { }

    public void InitializationMethod() { }

    public void UpdateMethod() => UpdateCollisionSystem(BulletManager.Instance.BulletsDict, UnitManager.Instance.UnitsDict, PlayerController.Instance);
}
