using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BulletManager : SingletonMono<BulletManager>
{
    public Dictionary<string, Queue<Bullet>> BulletsDict { get; private set; }
    private BulletManager() { }
    private readonly string path = "prefabs/BulletTypes/";
    public bool BATCHING_STATE { get; private set; }

    private void Awake()
    {
        ///// How it is going to work
        ///// Instead of keeping track of BulletType, we are going to put into a single folder all sub-script
        ///// defining a bullet type and Resources.LoadAll inside the dependency in order to retrieve their string name onStart\
        ///// and fill the dictionnary
        BulletsDict = new Dictionary<string, Queue<Bullet>>();
        BATCHING_STATE = false;
    }

    private void Update()
    {
        ///// BulletManager is going Update Bullets every cycle
        UpdateBullets(BulletsDict);
    }

    private void UpdateBullets(Dictionary<string, Queue<Bullet>> bulletsDict)
    {
        ///// Problem with the way I handle batching
        ///// I update the first bullet, third bullet, fifth bullet, etc...
        ///// than I update the opposite bullet
        ///// What I think will happen is some kind of stutter visual effect
        ///// 
        ///// Problem with the way I update is that I am going to pool value and go to the next index
        ///// At the same time I will be removing from it causing issues with the looping
        ///// it will either break the loop OR skip a bullet
        BATCHING_STATE = !BATCHING_STATE;
        foreach (var value in bulletsDict.Keys.SelectMany(key => bulletsDict[key]).Where(value => value.ID && BATCHING_STATE))
        {
            value.UpdateBulletPosition();
        }
    }
}
