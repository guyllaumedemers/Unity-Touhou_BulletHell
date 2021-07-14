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

    public IDictionary<int, Queue<(string, int)>> waveDict = new Dictionary<int, Queue<(string, int)>>();

    public int stageSelection { get; private set; }
    public int curr_dir { get; private set; }
    public int pivot_point { get; private set; }
    public int variable_mod { get; private set; }

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

    // Update Orientation allows to loop over the SpawningPosEnum and create variation in which unit spawn from
    private int UpdateDir(bool skip)
    {
        if (!skip)
        {
            curr_dir += curr_dir;
            if (curr_dir % pivot_point == 0) curr_dir = 1;
            return curr_dir;
        }
        return LateDirUpdate(curr_dir);
    }

    private int LateDirUpdate(int value)
    {
        curr_dir += value;
        if (curr_dir % pivot_point == 0) curr_dir = 1;
        return value;
    }

    /**********************FLOW****************************/

    //TODO PreIntilizationMethod will be called from the UI menu selection when the user select the stage
    public void PreIntilizationMethod(int stageselect, int startingDir, int pivot, int var_mod)
    {
        stageSelection = stageselect;
        curr_dir = startingDir;
        pivot_point = pivot;
        variable_mod = var_mod;
        waveDict = Tool.XMLDeserialization_KVPTuple(Globals.XMLLevelinfo);
    }

    //TODO Vector3 position for the launch function must be set depending on the waypoint of the unit so the unit comes in the opposite direction from it
    public IEnumerator InitializationMethod()
    {
        while (waveDict[stageSelection].Count > 0)
        {
            Launch<Unit>(waveDict[stageSelection].First().Item1, new Vector3(10, 5, 0), BulletTypeEnum.Circle, (SpawningPosEnum)UpdateDir(curr_dir % variable_mod == 2),
                0, waveDict[stageSelection].First().Item2, Globals.initializationInterval);
            RemoveEntry();
            yield return new WaitForSeconds(Globals.waveInterval);
        }
        // trigger event to go back to menu or to trigger next level
    }

    /***************DATA STRUCTURE MANAGEMENT********************/

    private void RemoveEntry() => waveDict[stageSelection].Dequeue();
}
