using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    private void Awake()
    {
        FactoryManager.Instance.PreIntilizationMethod();
        ObjectPool.PreInitializeMethod();
        PlayerController.Instance.PreIntilizationMethod();
        BulletManager.Instance.PreIntilizationMethod();
    }

    private void Start()
    {
        PlayerController.Instance.InitializationMethod();
    }

    private void Update()
    {
        PlayerController.Instance.UpdateMethod();
        BulletManager.Instance.UpdateMethod();
    }
}
