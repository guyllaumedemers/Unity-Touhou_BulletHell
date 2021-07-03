using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaveSystem : SingletonMono<WaveSystem>
{
    /*  Wave System goes as follow :
     * 
     *      Keep in mind that in a touhou game there is no randomization
     *      
     *      How it should work is that until the player die OR the stage level isnt complete, I want to run the wave system
     *      BUT
     *      I do not want to update it at every execution order cycle
     *      I want to be able to :
     *      
     *              run the initial wave
     *              and keep on running until the level is complete
     *              
     *              for each wave instance, a timer is set to trigger the next wave
     * 
     */

    private WaveSystem() { }

    private Dictionary<int, Dictionary<string, int>> waveDict = new Dictionary<int, Dictionary<string, int>>()
    {
        {0, new Dictionary<string, int>(){          // stage 0 :
                {Globals.sunflowerFairy, 5 },       // left
                {Globals.zombieFairy, 2 },          // right
                {Globals.boss, 1 }
            }
        }
    };

    /**********************ACTIONS**************************/

    private void Launch<T>(string name, Vector3 pos, BulletTypeEnum bulletType, SpawningPosEnum spEnum,
        int level, int maxUnitWave, float interval) where T : class
    {
        if (spEnum != SpawningPosEnum.Both)
        {
            StartCoroutine(UnitManager.Instance.SequencialInit<T>(name, pos, bulletType, spEnum, level, maxUnitWave, interval));
            return;
        }
        StartCoroutine(UnitManager.Instance.SequencialInit<T>(name, Vector3.zero - pos, bulletType, SpawningPosEnum.Left, level, maxUnitWave / 2, interval));
        StartCoroutine(UnitManager.Instance.SequencialInit<T>(name, Vector3.zero + pos, bulletType, SpawningPosEnum.Right, level, maxUnitWave / 2, interval));
    }

    public int stageSelection { get; private set; }
    public int curr_dir { get; private set; }
    public int pivot_point { get; private set; }

    /**********************FLOW****************************/

    public void PreIntilizationMethod(int stageselect, int startingDir, int pivot)
    {
        // will be set in the menu selection
        stageSelection = stageselect;
        curr_dir = startingDir;
        pivot_point = pivot;
    }

    public IEnumerator InitializationMethod()
    {
        /*  How to handle the spawning direction update?
         * 
         *          Garbage way -> I could have a queue that pop thru the enum values and return the current spawing pos to be at
         *          but that sucks
         * 
         *          Bullet management could also be handled that way -> altho it involves having 2 sets of data structures just for
         *          managing the params of the instantiation function
         *          
         *          How can I manage better the interval on which each wave are called?
         *          
         *          How can I instanciate two sides at the same time?
         *          
         *          TEMP solution for the initial position to spawn -> still have to fix the waypoint system function GetlevelWPpos
         */
        while (waveDict[stageSelection].Keys.Count > 0)
        {
            Launch<Unit>(waveDict[stageSelection].First().Key, new Vector3(10, 5, 0), BulletTypeEnum.Circle, (SpawningPosEnum)UpdateOrientation(false), 0,
                    waveDict[stageSelection].First().Value, Globals.initializationInterval);
            RemoveEntry();
            yield return new WaitForSeconds(Globals.waveInterval);
        }
        // do something -> new level -> go back menu -> from calling game manager -> trigger event
    }

    /***************DATA STRUCTURE MANAGEMENT********************/

    private void RemoveEntry() => waveDict[stageSelection].Remove(waveDict[stageSelection].First().Key);

    private int UpdateOrientation(bool skip)
    {
        /*      The idea here is to be able to loop over the choice and create variation without the mean of a queue for direction management
         *              pivot point represent the value at which we loop back to being SpawningEnum.left
         *                      an args for skipping is available when we try to create variation -> must be parametrize outside the function
         */
        if (skip) return curr_dir;
        else
        {
            curr_dir += curr_dir;
            if (curr_dir % pivot_point == 0) curr_dir = (curr_dir % pivot_point) + 1;
            return curr_dir;
        }
    }
}
