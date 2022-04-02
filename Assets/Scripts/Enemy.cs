using System;
using System.Collections;
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

    public float moveSpeed = 3f;
    protected AIDestinationSetter _setter;
    protected AIPath _aiPath;
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

    private void Start()
    {
        _setter = GetComponent<AIDestinationSetter>();
        _aiPath = GetComponent<AIPath>();
        _aiPath.maxSpeed = moveSpeed;
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





}
