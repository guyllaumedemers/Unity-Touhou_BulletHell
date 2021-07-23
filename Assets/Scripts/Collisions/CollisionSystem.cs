using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CollisionSystem : SingletonMono<CollisionSystem>, IFlow
{
    private Queue<IProduct> pool;

    #region Collision System Functions

    public bool DistanceCheck(Vector2 pos, Vector2 target, float rad) => Vector2.Distance(pos, target) <= rad;

    //// O(N^2) => R-tree might be the better solution for the lookup
    private void UpdateCollisionSystem(Dictionary<string, HashSet<Bullet>> bulletsDict, Dictionary<string, HashSet<Unit>> unitsDict, Queue<IProduct> pool, PlayerController player)
    {
        foreach (var bullet in bulletsDict.Keys.SelectMany(key => bulletsDict[key]))
        {
            if (bullet.ignoredLayer != IgnoreLayerEnum.Player)
            {
                // do distance check with the player
                if (DistanceCheck(bullet.transform.position, player.transform.position, player.rad))
                {
                    player.TakeDamage(bullet.dmg);
                    pool.Enqueue(bullet);
                }
            }
            else
            {
                // do distance check with the units
                foreach (var unit in unitsDict.Keys.SelectMany(key => unitsDict[key]).Where(x =>
                DistanceCheck(bullet.transform.position, x.transform.position, x.rad)))
                {
                    unit.TakeDamage(bullet.dmg);
                    pool.Enqueue(bullet);
                }
            }
        }
        while (pool.Count > 0) (pool.Dequeue() as Bullet).Pool();
    }

    #endregion

    #region Unity Functions

    public void PreIntilizationMethod() => pool = new Queue<IProduct>();

    public void InitializationMethod() { }

    public void UpdateMethod() => UpdateCollisionSystem(BulletManager.Instance.BulletsDict, UnitManager.Instance.UnitsDict, pool, PlayerController.Instance);

    #endregion
}
