using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaveSystem : SingletonMono<WaveSystem>, IFlow
{
    /*  Wave System goes as follow :
     * 
     *      Keep in mind that in a touhou game there is no randomization
     * 
     */

    private WaveSystem() { }

    private Dictionary<int, Dictionary<string, int>> waveDict = new Dictionary<int, Dictionary<string, int>>()
    {
        {0, new Dictionary<string, int>(){
                {Globals.sunflowerFairy, 6 },
                {Globals.zombieFairy, 2 }
            }
        },
        {1, new Dictionary<string, int>(){
                {Globals.sunflowerFairy, 12 },
                {Globals.zombieFairy, 4 }
            }
        },
        {2, new Dictionary<string, int>(){
                {Globals.boss, 1 },
            }
        }
    };

    /**********************ACTIONS**************************/

    private void Launch<T>(string name, Vector3 pos, BulletTypeEnum bulletType, SpawningPosEnum spEnum,
        int level, int maxUnitWave, float interval) where T : class
    {
        StartCoroutine(UnitManager.Instance.SequencialInit<T>(name, pos, bulletType, spEnum, level, maxUnitWave, interval));
    }

    public int currentWave { get; private set; }

    /**********************FLOW****************************/

    public void PreIntilizationMethod() { }

    public void InitializationMethod()
    {
        StartCoroutine(Utilities.Timer(3.0f, () =>
        {
            Launch<Unit>(waveDict[currentWave].First().Key, Vector3.zero, BulletTypeEnum.Circle, SpawningPosEnum.Left, 0,
                waveDict[currentWave].First().Value / 2, Globals.initializationInterval);
        }));
    }

    public void UpdateMethod()
    {
        // the wave manager has to instanciate a number of units for the length of a wave
        // a wave consist of multiple phase which instantiate half the unit per side for the current key entry
        // when the key entry is at 0
        // the key is removed -> problem, this doesnt allow reusability of the set of enemies in the dictionary

        // questions are : how will I update the args for the units like : BulletType and SpawingPos
    }
}
