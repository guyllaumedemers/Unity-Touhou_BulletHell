using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Unit : MonoBehaviour, IDamageable
{
    public IPatternGenerator pattern;
    public IMoveable moveable = new MoveableUnitBehaviour();
    public readonly IEnumFiltering enumFiltering = new EnumFilteringBehaviour();
    public readonly ISwappable bullets = new SwappablePatternBehaviour();
    public Coroutine fireCoroutine;
    // Unit values
    public float rad { get; private set; }
    public float health;
    public float speed;
    public float angle;
    public string activeBullet;
    public Queue<string> bulletType;
    private float bezierCurveT;
    private Vector3[] controlPoints;
    private int curr_wp = 0;
    private bool idle = false;

    /**********************ACTIONS**************************/

    // Bullet Type is now pass as arguments so i can parametrize the instanciation of an unit type directly in the function call instead of having values all around and overwriting
    public Unit PreInitializeUnit(BulletTypeEnum bulletT)
    {
        bulletType = new Queue<string>();
        controlPoints = new Vector3[3];
        transform.position = new Vector3(1, 1, 1);
        for (int i = 0; i < controlPoints.Length; i++)                                       // Temp solution
        {
            // some units are using the waypoints that are left while others use the right
            // how can I assign which waypoint needs to be used
            controlPoints[i] = WaypointSystem.Instance.Waypoints[i].Pos;
        }
        bezierCurveT = 0.0f;
        speed = 0.5f;
        rad = Globals.hitbox;
        foreach (var obj in FactoryManager.Instance.FactoryBullets.Where(x => enumFiltering.EnumToString(bulletT).Any(w => w.Equals(x.name)))) bulletType.Enqueue(obj.name);
        activeBullet = bullets.SwapBulletType(bulletType);                                                                      // initialize the active bullet type string    
        pattern = bullets.SwapPattern((BulletTypeEnum)System.Enum.Parse(typeof(BulletTypeEnum), activeBullet));                 // initialize the pattern with the active bullet type
        return this;
    }

    public void UpdateUnit()
    {
        if (!idle) Move();
    }

    private void Move()
    {
        if (Vector3.Distance(transform.position, controlPoints[curr_wp + 1 > 2 ? 0 : curr_wp + 1]) < Globals.min_wpDist)
        {
            ++curr_wp;
            idle = true;
            curr_wp %= controlPoints.Length;
            StartCoroutine(Utilities.Timer(Globals.idl_time, () => { return idle = false; }));
            bezierCurveT = 0.0f;    // reset bezier curve
            return;
        }
        bezierCurveT = bezierCurveT + Time.deltaTime * speed % 1.0f;
        transform.position = moveable.Move(default, default, bezierCurveT, controlPoints[curr_wp], controlPoints[curr_wp + 1 > 2 ? 0 : curr_wp + 1]);
    }

    public IEnumerator Play()
    {
        while (true)
        {
            pattern.Fill(activeBullet, null, transform.position, 0, 0);
            yield return new WaitForSeconds(1 / (pattern as AbsPattern).rof);
        }
    }

    public void StartFiring() => fireCoroutine = StartCoroutine(Play());

    public void StopFiring()
    {
        if (fireCoroutine != null) StopCoroutine(fireCoroutine);
    }

    public void TakeDamage(float dmg) => health -= dmg;

    /**********************DISABLE**************************/

    public void OnBecameInvisible() => StopFiring();
}
