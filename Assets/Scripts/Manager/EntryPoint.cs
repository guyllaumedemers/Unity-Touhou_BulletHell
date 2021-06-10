using System.Collections;
using System.Collections.Generic;
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
        UnitManager.Instance.PreIntilizationMethod();
        WaypointSystem.Instance.PreIntilizationMethod();
        CollisionSystem.Instance.PreIntilizationMethod();
    }

    private void Start()
    {
        StartCoroutine(ObjectPool.Trim());
        StartCoroutine(CollisionSystem.Instance.EmptyBulletColliderQueue());
        PlayerController.Instance.InitializationMethod();
        UnitManager.Instance.InitializationMethod();
    }

    private void Update()
    {
        PlayerController.Instance.UpdateMethod();
        BulletManager.Instance.UpdateMethod();
        UnitManager.Instance.UpdateMethod();
        CollisionSystem.Instance.UpdateMethod();
    }
}
