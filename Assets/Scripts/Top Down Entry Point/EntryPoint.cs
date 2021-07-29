using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EntryPoint : SingletonMono<EntryPoint>
{
    private EntryPoint() { }
    public float Last { get; private set; }

    public void Awake()
    {
        Tuple<string, int>[] myTest = DatabaseHandler.RetrieveTableEntries<Tuple<string, int>>(Globals.waveTable, $"WHERE Id = {0}");
        for (int i = 0; i < myTest.Length; ++i)
        {
            Debug.Log($"{myTest[i].Item1} {myTest[i].Item2}");
        }
        Vector3[] vec3 = Tool.CustomVec3Unwrapper(DatabaseHandler.RetrieveTableEntries<Tool.Vector3Wrapper>(Globals.waypointTable, $"WHERE Id = {0} AND Direction = '{SpawningPosEnum.Both.ToString()}'"));
        for (int i = 0; i < vec3.Length; ++i)
        {
            Debug.Log($"{vec3[i]}");
        }
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            AudioManager.Instance.PreIntilizationMethod();
            UIManager.Instance.PreIntilizationMethod();
            return;
        }
        GameManagerFunctionWrapper.StartGame();
        Last = default;
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            AudioManager.Instance.InitializationMethod();
            UIManager.Instance.InitializationMethod();
            return;
        }
        StartCoroutine(ObjectPool.Trim());
        GameManagerFunctionWrapper.InitializeGame();
        // level selection should be done from menu
        StartCoroutine(Utilities.Timer(Globals.waveInterval, () => { StartCoroutine(WaveSystem.Instance.StartWave(default, (int)SpawningPosEnum.None, (int)SpawningPosEnum.Pivot, 4)); }));
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            return;
        }
        if (Time.time - Last > Globals.fps)
        {
            GameManagerFunctionWrapper.UpdateMethod();
            Last = Time.time;
        }
    }
}
