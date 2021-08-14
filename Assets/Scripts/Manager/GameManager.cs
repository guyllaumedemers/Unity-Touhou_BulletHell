using UnityEngine;

public class GameManager : SingletonMono<GameManager>
{
    private ParticleSystem particule;
    private float last;
    public void Awake()
    {
        particule = FindObjectOfType<ParticleSystem>();
        if (!particule)
        {
            LogWarning("There is no particule system in the scene");
            return;
        }
        DestroyImmediate(particule);
        FactoryManager.Instance.PreIntilizationMethod();
        ObjectPool.PreInitializeMethod();
        CollisionSystem.Instance.PreIntilizationMethod();
        PlayerController.Instance.PreIntilizationMethod();
        BulletManager.Instance.PreIntilizationMethod();
        UnitManager.Instance.PreIntilizationMethod();
        last = Time.time;
    }

    public void Start()
    {
        PlayerController.Instance.InitializationMethod();
        StartCoroutine(WaveSystem.Instance.StartWave(0, (int)DirectionEnum.None, (int)DirectionEnum.Pivot, 4));
    }

    public void Update()
    {
        if (Time.time - last >= Globals.fps)
        {
            PlayerController.Instance.UpdateMethod();
            CollisionSystem.Instance.UpdateMethod();
            BulletManager.Instance.UpdateMethod();
            UnitManager.Instance.UpdateMethod();
            last = Time.time;
        }
    }

    #region private functions

    private void LogWarning(string msg) => Debug.LogWarning("[Title Screen Behaviour] : " + msg);

    #endregion
}
