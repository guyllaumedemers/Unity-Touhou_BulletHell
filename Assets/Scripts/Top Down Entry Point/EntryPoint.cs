using UnityEngine;
using UnityEngine.SceneManagement;

public class EntryPoint : SingletonMono<EntryPoint>
{
    private EntryPoint() { }
    public float Last { get; private set; }

    public void Awake()
    {
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
        StartCoroutine(Utilities.Timer(Globals.waveInterval, () => { StartCoroutine(WaveSystem.Instance.StartWave(default, (int)DirectionEnum.None, (int)DirectionEnum.Pivot, 4)); }));
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
