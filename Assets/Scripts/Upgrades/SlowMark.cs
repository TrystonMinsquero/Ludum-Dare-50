
using System.Collections;
using UnityEngine;

[System.Serializable]
public class SlowMark : Mark
{
    public override IEnumerator ApplyMark(Enemy enemy)
    {
        enemy.SetSpeed(enemy.moveSpeedInit / 2);
        isActive = true;
        yield return new WaitForSeconds(duration);
        enemy.SetSpeed(enemy.moveSpeedInit);
        isActive = false;
    }
}