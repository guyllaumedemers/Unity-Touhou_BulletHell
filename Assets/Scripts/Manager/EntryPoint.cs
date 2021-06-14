using UnityEngine;

public class EntryPoint : SingletonMono<EntryPoint>
{
    private EntryPoint() { }

    private void Awake()
    {
        FactoryManager.Instance.PreIntilizationMethod();
        ObjectPool.PreInitializeMethod();
        PlayerController.Instance.PreIntilizationMethod();
        BulletManager.Instance.PreIntilizationMethod();
        WaypointSystem.Instance.PreIntilizationMethod();
        UnitManager.Instance.PreIntilizationMethod();
        CollisionSystem.Instance.PreIntilizationMethod();
    }

    private void Start()
    {
        StartCoroutine(ObjectPool.Trim());
        PlayerController.Instance.InitializationMethod();
        UnitManager.Instance.InitializationMethod();
    }

    private void Update()
    {
        PlayerController.Instance.UpdateMethod();
        CollisionSystem.Instance.UpdateMethod();
        UnitManager.Instance.UpdateMethod();
        BulletManager.Instance.UpdateMethod();
    }
}
