using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(SpriteRenderer))]
public abstract class Unit : MonoBehaviour, IDamageable
{
    private Coroutine fireCoroutine;
    private SpriteRenderer spriteRen;
    private Animator animator;
    private float bezierCurveT;
    public UnitDataContainer unitData;

    #region Unit Functions

    public Unit PreInitializeUnit(string type, IMoveable move_behaviour, Vector3[] waypoints, BulletTypeEnum bulletT)
    {
        StaticInitialization(type, move_behaviour, waypoints);
        foreach (var obj in FactoryManager.Instance.FactoryBullets.Where(x => EnumFiltering.EnumToString(bulletT).Any(w => w.Equals(x.name))))
        {
            unitData.bulletType.Enqueue(obj.name);
        }
        unitData.activeBullet = unitData.bullets.SwapBulletType(unitData.bulletType);                                                           // initialize the active bullet type string    
        unitData.pattern = unitData.bullets.SwapPattern((BulletTypeEnum)System.Enum.Parse(typeof(BulletTypeEnum), unitData.activeBullet));      // initialize the pattern with the active bullet type
        return this;
    }

    public void UpdateUnit()
    {
        if (!unitData.idle) Move();
        unitData.animation.Animate(animator, spriteRen, (unitData.controlPoints[unitData.curr_wp + 1 > unitData.controlPoints.Length - 1 ? 0 : unitData.curr_wp + 1] - unitData.controlPoints[unitData.curr_wp]).normalized);
    }

    private void Move()
    {
        if (Vector3.Distance(transform.position, unitData.controlPoints[unitData.controlPoints.Length - 1]) < Globals.minWPDist)
        {
            unitData.hasReachDestination = true;
        }
        else if (Vector3.Distance(transform.position, unitData.controlPoints[unitData.curr_wp + 1 > unitData.controlPoints.Length - 1 ? 0 : unitData.curr_wp + 1]) < Globals.minWPDist)
        {
            ++unitData.curr_wp;
            unitData.curr_wp %= unitData.controlPoints.Length;
            unitData.idle = !unitData.idle;
            bezierCurveT = default;
            //TODO NEED to find a way to make the idl time variable according to the unit AND the level we are currently in OR wave we are at
            StartCoroutine(Utilities.Timer(Globals.idleTime, () => { unitData.idle = !unitData.idle; }));
            return;
        }
        bezierCurveT = bezierCurveT + Time.deltaTime * unitData.speed % 1.0f;
        transform.position = UpdateUnitPosition(unitData.moveable, unitData.curr_wp, unitData.controlPoints);
    }

    private Vector3 UpdateUnitPosition(IMoveable move_beahaviour, int curr_wp, Vector3[] waypoints)
    {
        if (Utilities.CheckInterfaceType(move_beahaviour, typeof(MoveableUnitLinearBezierB)))
        {
            return unitData.moveable.Move(default, default, bezierCurveT, waypoints[curr_wp > waypoints.Length - 1 ? 0 : curr_wp], waypoints[curr_wp + 1 > waypoints.Length - 1 ? 0 : curr_wp + 1]);
        }
        return unitData.moveable.Move(default, default, bezierCurveT, waypoints[0], waypoints[1], waypoints[2], waypoints[3]);
    }

    public IEnumerator DriftOff()
    {
        int dir = unitData.controlPoints[0].x < 0 ? 1 : -1;
        while (unitData.idle)
        {
            transform.position += new Vector3(0.5f * dir, -0.3f, 0.0f) * Time.deltaTime;
            yield return null;
        }
    }

    public IEnumerator Play()
    {
        while (true)
        {
            unitData.pattern.Fill(unitData.activeBullet, null, transform.position, default, default);
            yield return new WaitForSeconds(1 / (unitData.pattern as AbsPattern).rof);
        }
    }

    public void StartFiring() => fireCoroutine = StartCoroutine(Play());

    public void StopFiring()
    {
        if (fireCoroutine != null) StopCoroutine(fireCoroutine);
    }

    public void TakeDamage(float dmg) => unitData.health -= dmg;

    #endregion

    #region Unit Initialization Wrapper

    private void StaticInitialization(string type, IMoveable move_behaviour, Vector3[] waypoints)
    {
        spriteRen = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        //TODO Load Unit data instance from a file by type with unit info
        unitData = new UnitDataContainer(1.0f, 100.0f, 2.0f, false, 0, null, waypoints, false, new Queue<string>(), null, move_behaviour);
        bezierCurveT = default;
    }

    public UnitDataContainer GetUnitData() => unitData;

    #endregion
}

public struct UnitDataContainer
{
    public float rad;
    public float health;
    public float speed;
    public bool idle;
    public int curr_wp;
    public string activeBullet;
    public Vector3[] controlPoints;
    public bool hasReachDestination;
    public Queue<string> bulletType;
    public IPatternGenerator pattern;
    public IMoveable moveable;
    public IAnimate animation;
    public ISwappable bullets;

    public UnitDataContainer(float rad, float health, float speed, bool idle, int curr_wp, string activeBullet, Vector3[] controlPoints, bool hasReachDestination,
        Queue<string> bulletType, IPatternGenerator pattern, IMoveable moveable)
    {
        this.rad = rad;
        this.health = health;
        this.speed = speed;
        this.idle = idle;
        this.curr_wp = curr_wp;
        this.activeBullet = activeBullet;
        this.controlPoints = controlPoints;
        this.hasReachDestination = hasReachDestination;
        this.bulletType = bulletType;
        this.pattern = pattern;
        this.moveable = moveable;
        this.animation = new UnitAnimationBehaviour();
        this.bullets = new SwappablePatternBehaviour();
    }
}