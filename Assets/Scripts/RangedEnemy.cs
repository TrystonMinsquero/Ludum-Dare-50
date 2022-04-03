using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Enemy
{
    [Header("Ranged")] 
    public GameObject projectile;
    public float attackRange;
    public float projectTileSpeed;
    public Transform projectTileLaunchPoint;
    
    protected override void Update()
    {
        base.Update();
        if ( _aiPath.reachedDestination && !chargingUp && canAttackTime <= Time.time && _setter.target != null)
            StartCoroutine(ChargeUpThenAttack(chargeUpTime));
    }

    protected override IEnumerator Attack()
    {
        if (_setter.target != null)
        {
            isAttacking = true;
        }
        isAttacking = false;
        yield return null;
        EndAttack();
    }

    private void OnDrawGizmosSelected()
    {
    }

    protected override void SetAnimation()
    {
        if (_aiPath.velocity.x > 0)
            _sr.flipX = true;
        else if(_aiPath.velocity.x < 0)
            _sr.flipX = false;
        
        string stateName = enemyName;
        if (isDying)
            stateName += "Death";
        else if (chargingUp)
            stateName += "Attack";
        else if (_aiPath.velocity.magnitude > .05f)
            stateName += "Walk";
        else
            stateName += "Idle";
        _anim.Play(stateName);
    }
}
