
using System.Collections;
using UnityEngine;

[System.Serializable]
public class SlowMark : Mark
{
    public ModiferType modiferType;
    public float value;
    
    public override IEnumerator ApplyMark(Enemy enemy)
    {
        float prevChargeTime = enemy.chargeUpTime;
        enemy.SetSpeed(ModiferHelper.ApplyModifer(enemy.moveSpeedInit, value, modiferType));
        enemy.chargeUpTime = (ModiferHelper.ApplyModifer(prevChargeTime, value, modiferType));
        float endTime = Time.time + duration;
        while (Time.time <= endTime)
        {
            if (!enemy.IsStuned())
            {
                enemy.SetSpeed(ModiferHelper.ApplyModifer(enemy.moveSpeedInit, value, modiferType));
                enemy.chargeUpTime = (ModiferHelper.ApplyModifer(prevChargeTime, value, modiferType));
            }
            yield return null;
        }
        enemy.SetSpeed(enemy.moveSpeedInit);
        enemy.chargeUpTime = prevChargeTime;
        IsActive = false;
    }
    
}