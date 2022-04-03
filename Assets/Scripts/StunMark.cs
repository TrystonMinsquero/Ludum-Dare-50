
using System.Collections;
using UnityEngine;

public class StunMark : Mark
{
    public override IEnumerator ApplyMark(Enemy enemy)
    {
        enemy.SetSpeed(0);
        isActive = true;
        yield return new WaitForSeconds(duration);
        enemy.SetSpeed(enemy.moveSpeedInit);
        isActive = false;
    }
}