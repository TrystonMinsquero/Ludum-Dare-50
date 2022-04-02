
using System.Collections;
using UnityEngine;

public class StunMark : Mark
{
    public override IEnumerator ApplyMark(Enemy enemy)
    {
        enemy.moveSpeed /= 2;
        yield return new WaitForSeconds(duration);
        enemy.moveSpeed *= 2;
    }
}