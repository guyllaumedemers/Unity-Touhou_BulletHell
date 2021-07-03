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
                {Globals.zombieFairy, 1 },          // right
                {Globals.boss, 1 }
            }
        }
    };

    /**********************ACTIONS**************************/

    private void Launch<T>(string name, Vector3 pos, BulletTypeEnum bulletType, SpawningPosEnum spEnum,
        int level, int maxUnitWave, float interval) where T : class
    {
        StartCoroutine(UnitManager.Instance.SequencialInit<T>(name, pos, bulletType, spEnum, level, maxUnitWave, interval));
    }

    public int stageSelection { get; private set; }

    /**********************FLOW****************************/

    public void PreIntilizationMethod(int stageselect)
    {
        // will be set in the menu selection
        stageSelection = stageselect;
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
         */
        while (waveDict[stageSelection].Keys.Count > 0)
        {
            Launch<Unit>(waveDict[stageSelection].First().Key, Vector3.zero, BulletTypeEnum.Circle, SpawningPosEnum.Left, 0,
                    waveDict[stageSelection].First().Value, Globals.initializationInterval);
            RemoveEntry();
            yield return new WaitForSeconds(Globals.waveInterval);
        }
    }

    /***************DICTIONARY MANAGEMENT********************/

    private void RemoveEntry() => waveDict[stageSelection].Remove(waveDict[stageSelection].First().Key);
}
