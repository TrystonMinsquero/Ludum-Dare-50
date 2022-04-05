using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    [Header("Melee")]
    public float attackRadius = .5f;
    public float attackRange = .5f;
    public GameObject slamAnim;

    
    protected override void Update()
    {
        base.Update();
        if (_aiPath.reachedDestination && !chargingUp && !isAttacking && canAttackTime <= Time.time &&
            _setter.target != null)
        {
            chargingUp = true;
            StartCoroutine(ChargeUpThenAttack(chargeUpTime));
        }
    }

    protected override IEnumerator Attack()
    {
        if (_setter.target != null)
        {
            isAttacking = true;
            Vector3 attackPoint = (_setter.target.position - transform.position).normalized * attackRange;
            
            //Slam animation
            slamAnim.transform.position = transform.position + attackPoint;
            slamAnim.GetComponent<Animator>().Play(enemyName + "Slam");

            Collider2D[] collisions = Physics2D.OverlapCircleAll(transform.position + attackPoint, attackRadius);
            
            foreach(Collider2D collision in collisions)
                if (collision.TryGetComponent<Player>(out var player))
                {
                    player.TakeDamage(damageAmount);
                    break;
                }
        }

        EndAttack();
        yield return null;
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 attackPoint;
        if(!_setter && _setter.target == null)
            attackPoint= ((transform.position + Vector3.right)).normalized * attackRange;
        else
            attackPoint = ((_setter.target.position - transform.position).normalized) * attackRange;
        
        if (attackPoint != null)
            Gizmos.DrawWireSphere(transform.position + attackPoint, attackRadius);
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
        else if (isStunned)
            stateName += "Stun";
        else if (chargingUp)
            stateName += "Charge";
        else if (isAttacking)
            stateName += "Attack";
        else if (_aiPath.velocity.magnitude > .05f)
            stateName += "Walk";
        else
            stateName += "Idle";
        _anim.Play(stateName);
    }
}
