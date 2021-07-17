using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(SpriteRenderer))]
public abstract class Unit : MonoBehaviour, IDamageable
{
    public IPatternGenerator pattern;
    public IMoveable moveable;
    public readonly IEnumFiltering enumFiltering = new EnumFilteringBehaviour();
    public readonly IAnimate animationBehaviour = new UnitAnimationBehaviour();
    public readonly ISwappable bullets = new SwappablePatternBehaviour();
    public Coroutine fireCoroutine;
    // Unit values
    public float rad { get; private set; }
    public float health;
    public float speed;
    public float angle;
    public string activeBullet;
    public bool hasReachDestination = false;
    public Queue<string> bulletType;
    // Check later for encapsulation
    public float bezierCurveT;
    public Vector3[] controlPoints;
    public int curr_wp = default;
    public bool idle = default;
    public SpriteRenderer spriteRen;
    public Animator animator;

    /**********************ACTIONS**************************/

    // Bullet Type is now pass as arguments so i can parametrize the instanciation of an unit type directly in the function call
    // instead of having values all around and overwriting
    public Unit PreInitializeUnit(IMoveable move_behaviour, Vector3[] waypoints, BulletTypeEnum bulletT)
    {
        StaticInitialization(move_behaviour, waypoints);
        foreach (var obj in FactoryManager.Instance.FactoryBullets.Where(x => enumFiltering.EnumToString(bulletT).Any(w => w.Equals(x.name)))) bulletType.Enqueue(obj.name);
        activeBullet = bullets.SwapBulletType(bulletType);                                                                  // initialize the active bullet type string    
        pattern = bullets.SwapPattern((BulletTypeEnum)System.Enum.Parse(typeof(BulletTypeEnum), activeBullet));             // initialize the pattern with the active bullet type
        return this;
    }

    public void UpdateUnit()
    {
        if (!idle) Move();
        animationBehaviour.Animate(animator, spriteRen, (controlPoints[curr_wp + 1 > controlPoints.Length - 1 ? 0 : curr_wp + 1] - controlPoints[curr_wp]).normalized);
    }

    private void Move()
    {
        if (Vector3.Distance(transform.position, controlPoints[curr_wp + 1 > controlPoints.Length - 1 ? 0 : curr_wp + 1]) < Globals.minWPDist)
        {
            ++curr_wp;
            curr_wp %= controlPoints.Length;
            idle = !idle;
            bezierCurveT = default;
            hasReachDestination = true;
            //TODO NEED to find a way to make the idl time variable according to the unit AND the level we are currently in OR wave we are at
            if (Utilities.CheckInterfaceType(moveable, typeof(MoveableUnitLinearBezierB)))
            {
                StartCoroutine(DriftOff());
                StartCoroutine(Utilities.Timer(Globals.idleTime, () => { idle = !idle; }));
            }
            return;
        }
        bezierCurveT = bezierCurveT + Time.deltaTime * speed % 1.0f;
        transform.position = UpdateUnitPosition(moveable, curr_wp, controlPoints);
    }

    //TODO Fix problem with linear bezier curve // setting transform directly as first index isnt the right thing to do
    private Vector3 UpdateUnitPosition(IMoveable move_beahaviour, int curr_wp, Vector3[] waypoints)
    {
        if (Utilities.CheckInterfaceType(move_beahaviour, typeof(MoveableUnitLinearBezierB)))
        {
            return moveable.Move(default, default, bezierCurveT, transform.position, waypoints[curr_wp + 1 > waypoints.Length - 1 ? 0 : curr_wp + 1]);
        }
        return moveable.Move(default, default, bezierCurveT, waypoints[0], waypoints[1], waypoints[2], waypoints[3]);
    }

    public IEnumerator DriftOff()
    {
        int dir = controlPoints[0].x < 0 ? 1 : -1;
        while (idle)
        {
            transform.position += new Vector3(0.5f * dir, -0.3f, 0.0f) * Time.deltaTime;
            yield return null;
        }
    }

    public IEnumerator Play()
    {
        while (true)
        {
            pattern.Fill(activeBullet, null, transform.position, default, default);
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

    /***************STATIC INITIALIZATION*******************/

    private void StaticInitialization(IMoveable move_behaviour, Vector3[] waypoints)
    {
        spriteRen = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        bulletType = new Queue<string>();
        controlPoints = waypoints;
        bezierCurveT = default;
        speed = Globals.u_speed;
        rad = Globals.hitbox;
        moveable = move_behaviour;
    }
}
