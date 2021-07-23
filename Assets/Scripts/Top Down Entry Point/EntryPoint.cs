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
            PageController.Instance.PreIntilizationMethod();
            //UIManager.Instance.PreIntilizationMethod();
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
            PageController.Instance.InitializationMethod();
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
