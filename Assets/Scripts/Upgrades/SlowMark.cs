
using System.Collections;
using UnityEngine;

[System.Serializable]
public class SlowMark : Mark
{
    public override IEnumerator ApplyMark(Enemy enemy)
    {
        enemy.SetSpeed(enemy.moveSpeedInit / 2);
        IsActive = true;
        yield return new WaitForSeconds(duration);
        enemy.SetSpeed(enemy.moveSpeedInit);
        IsActive = false;
    }
}