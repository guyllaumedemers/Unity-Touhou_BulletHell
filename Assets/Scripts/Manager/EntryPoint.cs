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
        ObjectPool.Fill();
        PlayerController.Instance.PreIntilizationMethod();
        BulletManager.Instance.PreIntilizationMethod();
    }

    private void Start()
    {
        StartCoroutine(ObjectPool.Trim());
        PlayerController.Instance.InitializationMethod();
    }

    private void Update()
    {
        PlayerController.Instance.UpdateMethod();
        BulletManager.Instance.UpdateMethod();
    }
}
