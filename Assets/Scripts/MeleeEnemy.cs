using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    [Header("Melee")]
    public float attackRadius = .5f;
    public float attackRange = .5f;

    
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
            Vector3 attackPoint = (_setter.target.position - transform.position).normalized * attackRange;
            Collider2D[] collisions = Physics2D.OverlapCircleAll(transform.position + attackPoint, attackRadius);
            
            foreach(Collider2D collision in collisions)
                if (collision.TryGetComponent<Player>(out var player))
                {
                    player.TakeDamage(damageAmount);
                    break;
                }
        }
        yield return null;
        EndAttack();
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 attackPoint;
        if(_setter.target == null)
            attackPoint= ((transform.position + Vector3.right)).normalized * attackRange;
        else
            attackPoint = ((_setter.target.position - transform.position).normalized) * attackRange;
        
        if (attackPoint != null)
            Gizmos.DrawWireSphere(transform.position + attackPoint, attackRadius);
    }
}
