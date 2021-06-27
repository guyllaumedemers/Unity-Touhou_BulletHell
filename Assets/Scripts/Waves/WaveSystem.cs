using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    /*  Wave System goes as follow :
     * 
     *      We are going to keep track of the enemies to instanciate thru the dict
     *      each entry represent a wave :
     *      
     *      each wave has unit type, number of units for the type
     *      entries will represent the order in which they spawn
     * 
     */

    private static Dictionary<int, Dictionary<string, int>> waveDict = new Dictionary<int, Dictionary<string, int>>()
    {
        {0, new Dictionary<string, int>(){
                {"SunflowerFairy", 6 },
                {"ZombieFairy", 2 }
            }
        },
        {1, new Dictionary<string, int>(){
                {"SunflowerFairy", 6 },
                {"ZombieFairy", 2 },
                {"SunflowerFairy", 6 },
                {"ZombieFairy", 2 }
            }
        },
        {2, new Dictionary<string, int>(){
                {"Boss", 1 },
            }
        }
    };
}
