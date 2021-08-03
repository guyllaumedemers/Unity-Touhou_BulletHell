using UnityEngine;

public class GameSceneBehaviour : AbsSceneHandler
{
    private float last;
    public void Awake()
    {
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
}
