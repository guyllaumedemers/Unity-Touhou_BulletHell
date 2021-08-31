using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(SpriteRenderer))]
public class Unit : AbsUnit
{
    private Coroutine fireCoroutine;
    private SpriteRenderer spriteRen;
    private Animator animator;
    private float bezierCurveT;
    public UnitDataContainer unitData { get; private set; }

    #region interface
    public override void StartFiring(ref Coroutine routine)
    {
        if (routine != null)
        {
            StopFiring(ref routine);
        }
        routine = StartCoroutine(Play());
    }
    public override void StopFiring(ref Coroutine routine)
    {
        if (routine != null) StopCoroutine(routine);
    }
    public override void Shoot(Transform parent, Transform pos)
    {
        unitData.pattern.Fill(unitData.activeBullet, parent, pos.position, 0, 0);
        unitData.pattern.UpdateBulletPattern(default, default);
        foreach (Bullet b in (unitData.pattern as AbsPattern).bullets) b.SetIgnoredLayer(IgnoreLayerEnum.Unit);
    }
    public override void Move()
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

    public override void TakeDamage(float dmg)
    {
        throw new System.NotImplementedException();
    }
    #endregion

    #region private functions
    private IEnumerator Play()
    {
        while (true)
        {
            unitData.pattern.Fill(unitData.activeBullet, null, transform.position, default, default);
            yield return new WaitForSeconds(1 / (unitData.pattern as AbsPattern).rof);
        }
    }
    private Vector3 UpdateUnitPosition(IMoveable move_beahaviour, int curr_wp, Vector3[] waypoints)
    {
        if (Utilities.CheckInterfaceType(move_beahaviour, typeof(MoveableUnitLinearBezierB)))
        {
            return unitData.moveable.Move(default, default, bezierCurveT, waypoints[curr_wp > waypoints.Length - 1 ? 0 : curr_wp], waypoints[curr_wp + 1 > waypoints.Length - 1 ? 0 : curr_wp + 1]);
        }
        return unitData.moveable.Move(default, default, bezierCurveT, waypoints[0], waypoints[1], waypoints[2], waypoints[3]);
    }
    private void StaticInitialization(string type, IMoveable move_behaviour, Vector3[] waypoints)
    {
        spriteRen = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        unitData = DatabaseHandler.RetrieveTableEntries<UnitDataContainer>(SQLTableEnum.UnitData.ToString()).Where(x => x.unitType.ToString().Equals(type)).FirstOrDefault();
        unitData.SetMoveableAction(move_behaviour);
        unitData.SetWaypoints(waypoints);
        bezierCurveT = default;
    }
    #endregion


    public Unit PreInitializeUnit(string type, IMoveable move_behaviour, Vector3[] waypoints, BulletTypeEnum bulletT)
    {
        StaticInitialization(type, move_behaviour, waypoints);
        foreach (var obj in FactoryManager.Instance.FactoryBullets.Where(x => EnumFiltering.EnumToString(bulletT).Any(w => w.Equals(x.name))))
        {
            unitData.bulletTypeList.Enqueue(obj.name);
        }
        unitData.activeBullet = unitData.bullets.SwapBulletType(unitData.bulletTypeList);                                                       // initialize the active bullet type string    
        unitData.pattern = unitData.bullets.SwapPattern((BulletTypeEnum)System.Enum.Parse(typeof(BulletTypeEnum), unitData.activeBullet));      // initialize the pattern with the active bullet type
        return this;
    }
    public void UpdateUnit()
    {
        if (!unitData.idle) Move();
        unitData.animation.Animate(animator, spriteRen, (unitData.controlPoints[unitData.curr_wp + 1 > unitData.controlPoints.Length - 1 ? 0 : unitData.curr_wp + 1] - unitData.controlPoints[unitData.curr_wp]).normalized);
    }
}

public class UnitDataContainer
{
    public float rad;
    public float health;
    public float speed;
    public bool idle;
    public int curr_wp;
    public string activeBullet;
    public Vector3[] controlPoints;
    public bool hasReachDestination;
    public Queue<string> bulletTypeList;
    public IPatternGenerator pattern;
    public IMoveable moveable;
    public IAnimate animation;
    public ISwappable bullets;
    public UnitTypeEnum unitType;

    public UnitDataContainer(UnitTypeEnum unitType, float rad, float health, float speed, IMoveable moveable = null,
        int curr_wp = 0, Vector3[] controlPoints = null, bool idle = false, bool hasReachDestination = false)
    {
        this.unitType = unitType;
        this.rad = rad;
        this.health = health;
        this.speed = speed;
        this.idle = idle;
        this.curr_wp = curr_wp;
        this.activeBullet = null;
        this.controlPoints = controlPoints;
        this.hasReachDestination = hasReachDestination;
        this.bulletTypeList = new Queue<string>();
        this.pattern = null;
        this.moveable = moveable;
        this.animation = new UnitAnimationBehaviour();
        this.bullets = new SwappablePatternBehaviour();
    }

    public void SetMoveableAction(IMoveable move) => this.moveable = move;

    public void SetWaypoints(Vector3[] waypoints) => this.controlPoints = waypoints;
}