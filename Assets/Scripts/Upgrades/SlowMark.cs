
using System.Collections;
using UnityEngine;

[System.Serializable]
public class SlowMark : Mark
{
    public ModiferType modiferType;
    public float value;
    
    public override IEnumerator ApplyMark(Enemy enemy)
    {
        float prevSpeed = enemy.moveSpeedInit;
        float prevChargeTime = enemy.chargeUpTime;
        enemy.SetSpeed(ModiferHelper.ApplyModifer(prevSpeed, value, modiferType));
        enemy.chargeUpTime = (ModiferHelper.ApplyModifer(prevChargeTime, value, modiferType));
        yield return new WaitForSeconds(duration);
        enemy.SetSpeed(prevSpeed);
        enemy.chargeUpTime = prevChargeTime;
        IsActive = false;
    }
    
}