using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public abstract class Enemy : MonoBehaviour
{
    [Header("Generic")]
    public bool isMarked;
    public int damageAmount = 5;
    public float attackInterval = .1f;
    public float chargeUpTime;
    public bool canMoveWhileCharging;
    public float moveSpeedInit = 3f;
    public List<Mark> activeMarks;

    public float moveSpeed = 3f;
    protected AIDestinationSetter _setter;
    protected AIPath _aiPath;
    protected SpriteRenderer _markedSR;
    protected bool chargingUp;

    protected float TargetDistance
    {
        get
        {
            if (_setter.target == null)
                return Mathf.Infinity;
            else
                return (_setter.target.position - transform.position).magnitude;
        }
    }

    protected float canAttackTime;

    private Transform embeddedWeapon;
    private Vector3 embeddedWeaponOffset;

    protected virtual void Start()
    {
        _setter = GetComponent<AIDestinationSetter>();
        _aiPath = GetComponent<AIPath>();
        
        _markedSR = GetComponentsInChildren<SpriteRenderer>()[1];
        SetSpeed(moveSpeed);
        activeMarks = new List<Mark>();
    }

    protected virtual void Update()
    {
        if (embeddedWeapon)
            embeddedWeapon.position = embeddedWeaponOffset + transform.position;
        _markedSR.enabled = activeMarks.Count > 0;
    }

    protected IEnumerator ChargeUpThenAttack(float time)
    {
        Debug.Log($"{name} Charging Up!");
        chargingUp = true;
        if(!canMoveWhileCharging)
            _aiPath.canMove = false;
        yield return new WaitForSeconds(time);
        StartCoroutine(Attack());
        chargingUp = false;
    }

    public void SetTarget(Transform target)
    {
        _setter.target = target;
    }


    protected abstract IEnumerator Attack();

    protected void EndAttack()
    {
        
        Debug.Log($"{name} Attacked!");
        canAttackTime = Time.time + attackInterval;
        _aiPath.canMove = true;
    }

    public void SetSpeed(float speed)
    {
        _aiPath.maxSpeed = speed;
    }

    public void HitByWeapon(Weapon weapon)
    {
        embeddedWeapon = weapon.transform;
        embeddedWeaponOffset = (embeddedWeapon.position - transform.position);
        ReceiveMarks(weapon.marks);
    }

    public void ReceiveMarks(List<Mark> marks)
    {
        foreach (var mark in marks)
        {
            ReceiveMark(mark);
        }
    }

    public void ReceiveMark(Mark mark)
    {
        activeMarks.Add(mark);
        mark.isActive = true;
        StartCoroutine(mark.ApplyMark(this));
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.TryGetComponent<Player>(out var player))
        {
            if(embeddedWeapon)
                player.GetComponent<WeaponHolder>().PickUpWeapon(embeddedWeapon.GetComponent<Weapon>());
            if (player.GetComponent<PlayerMovement>().IsDashing() && embeddedWeapon)
                Destroy(gameObject);
            else
                player.TakeDamage(1);
            embeddedWeapon = null;
        }
    }
}
