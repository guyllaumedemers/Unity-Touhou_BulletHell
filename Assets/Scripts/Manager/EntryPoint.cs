using UnityEngine;

public class EntryPoint : SingletonMono<EntryPoint>
{
    private EntryPoint() { }

    private void Awake()
    {
        FactoryManager.Instance.PreIntilizationMethod();
        WaypointSystem.Instance.PreIntilizationMethod();
        WaveSystem.Instance.PreIntilizationMethod(0, 1, 8);
        ObjectPool.PreInitializeMethod();
        PlayerController.Instance.PreIntilizationMethod();
        BulletManager.Instance.PreIntilizationMethod();
        UnitManager.Instance.PreIntilizationMethod();
    }

    private void Start()
    {
        StartCoroutine(ObjectPool.Trim());
        PlayerController.Instance.InitializationMethod();
        StartCoroutine(Utilities.Timer(3.0f, () => { StartCoroutine(WaveSystem.Instance.InitializationMethod()); }));
    }

    private void Update()
    {
        PlayerController.Instance.UpdateMethod();
        CollisionSystem.Instance.UpdateMethod();
        BulletManager.Instance.UpdateMethod();
        UnitManager.Instance.UpdateMethod();
    }
}
