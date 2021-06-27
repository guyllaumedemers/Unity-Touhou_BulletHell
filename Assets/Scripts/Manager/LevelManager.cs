using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LevelManager
{
    /*  Level Magement goes as follow :
     * 
     *      It contains the informations regarding the number of waves for this specific level
     *      
     *      A level is broken down into multiple waves OR phases and trigger new units over time meaning events are invoke
     *      after a period of time to manage waves system
     * 
     *      Level Manager will be calling the wave system who has for only purpose to trigger unit instanciation after period of time
     *      It ALSO have to reset the waypoints between level
     */

    // first entry represent the level to instanciate and the second entry represent the number of waves
    private static Dictionary<int, int> levelDict = new Dictionary<int, int>()
    {
        {0, 2 },
        {1, 4 },
        {2, 1 }
    };

    public static int curr_lvl { get; private set; }
    public static void UpdateCurrntLevel() => ++curr_lvl;
    public static int GetWaveAt(int curr_level) => levelDict[curr_lvl];
}
