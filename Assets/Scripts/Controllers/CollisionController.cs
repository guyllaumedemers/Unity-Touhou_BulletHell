using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CollisionController
{
    private static CollisionController instance;
    private CollisionController() { }
    public static CollisionController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new CollisionController();
            }
            return instance;
        }
    }

    private Queue<Bullet> pool;

    public bool DistanceCheck(Vector2 pos, Vector2 target, float rad) => Vector2.Distance(pos, target) <= rad;

    //// O(N^2) => R-tree might be the better solution for the lookup
    private void UpdateCollisionSystem(Dictionary<string, HashSet<Bullet>> bulletsDict, Dictionary<string, HashSet<Unit>> unitsDict, Queue<Bullet> pool, PlayerController player)
    {
        foreach (var bullet in bulletsDict.Keys.SelectMany(key => bulletsDict[key]))
        {
            if (bullet.bulletData.ignoredLayer != IgnoreLayerEnum.Player)
            {
                // do distance check with the player
                if (DistanceCheck(bullet.transform.position, player.transform.position, player.unitData.rad))
                {
                    player.TakeDamage(bullet.bulletData.dmg);
                    pool.Enqueue(bullet);
                }
            }
            else
            {
                // do distance check with the units
                foreach (var unit in unitsDict.Keys.SelectMany(key => unitsDict[key]).Where(x =>
                DistanceCheck(bullet.transform.position, x.transform.position, x.unitData.rad)))
                {
                    unit.TakeDamage(bullet.bulletData.dmg);
                    pool.Enqueue(bullet);
                }
            }
        }
        while (pool.Count > 0) pool.Dequeue().Pool();
    }

    public void PreInitializeCollisionController() => pool = new Queue<Bullet>();

    public void UpdateCollisionController(PlayerController playerController)
    {
        if (!playerController)
        {
            LogWarning("Player controller is null");
            return;
        }
        UpdateCollisionSystem(BulletManager.Instance.BulletsDict, UnitManager.Instance.UnitsDict, pool, playerController);
    }

    private void LogWarning(string msg) => Debug.Log("[Collision Controller] : " + msg);
}
