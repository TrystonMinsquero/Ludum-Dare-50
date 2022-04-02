
using System.Collections;
using UnityEngine;

public class SlowMark : Mark
{
    public override IEnumerator ApplyMark(Enemy enemy)
    {
        enemy.moveSpeed = 0;
        yield return new WaitForSeconds(duration);
        enemy.moveSpeed = enemy.moveSpeedInit;
    }
}