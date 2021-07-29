using UnityEngine;
using UnityEngine.SceneManagement;

public class EntryPoint : SingletonMono<EntryPoint>
{
    private EntryPoint() { }
    public float Last { get; private set; }

    public void Awake()
    {
        Vector3[] pos = Tool.CustomVec3Unwrapper(DatabaseHandler.RetrieveTableEntries<Tool.Vector3Wrapper>(Globals.waypointTable, $"WHERE Id = {0} AND Direction = '{SpawningPosEnum.Left.ToString()}'"));
        for (int i = 0; i < pos.Length; ++i)
        {
            Debug.Log(pos[i]);
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
        StartCoroutine(Utilities.Timer(Globals.waveInterval, () => { StartCoroutine(WaveSystem.Instance.InitializationMethod()); }));
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
