using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyEnemy : Enemy
{
    protected override IEnumerator Attack()
    {
        isAttacking = true;
        yield return null;
        EndAttack();
    }

    protected override void SetAnimation()
    {
        string stateName = enemyName;
        if (isDying)
            stateName += "Death";
        else
            stateName += "Idle";
        _anim.Play(stateName);
    }
}
