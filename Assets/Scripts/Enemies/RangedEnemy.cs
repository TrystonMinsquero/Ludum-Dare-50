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
        if (!_setter.target)
            return;
        float targetDistance = (_setter.target.position - transform.position).magnitude;
        if ( targetDistance <= attackRange && !chargingUp && !isAttacking && canAttackTime <= Time.time && _setter.target != null)
            StartCoroutine(ChargeUpThenAttack(chargeUpTime));
    }

    protected override IEnumerator Attack()
    {
        if (_setter.target != null)
        {
            isAttacking = true;
            var projectile = Instantiate(this.projectile, projectTileLaunchPoint.position, Quaternion.identity).GetComponent<EnemyProjectile>();
            var direction = (_setter.target.transform.position - projectTileLaunchPoint.position).normalized;
            projectile.SetRotation(direction);
            projectile.Throw(direction, this);
        }
        yield return null;
        EndAttack();
    }

    private void OnDrawGizmosSelected()
    {
            Gizmos.DrawWireSphere(transform.position, attackRange);
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
            stateName += "Charge";
        else if (isAttacking)
            stateName += "Attack";
        else
        {
            if (canAttackTime <= Time.time)
                stateName += "Weapon";
            if (_aiPath.velocity.magnitude > .05f)
                stateName += "Walk";
            else
                stateName += "Idle";
        }
           
        _anim.Play(stateName);
    }
}
