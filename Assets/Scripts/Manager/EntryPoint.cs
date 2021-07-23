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
        GameManagerIntermediate.StartGame();
        Last = default;
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            AudioManager.Instance.InitializationMethod();
            return;
        }
        StartCoroutine(ObjectPool.Trim());
        GameManagerIntermediate.InitializeGame();
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
            GameManagerIntermediate.UpdateMethod();
            Last = Time.time;
        }
    }
}
