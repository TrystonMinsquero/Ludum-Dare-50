
using System.Collections;
using UnityEngine;

[System.Serializable]
public class SlowMark : Mark
{
    public ModiferType modiferType;
    public float value;
    
    public override IEnumerator ApplyMark(Enemy enemy)
    {
        float prevSpeed = enemy.moveSpeed;
        enemy.SetSpeed(ModiferHelper.ApplyModifer(prevSpeed, value, modiferType));
        IsActive = true;
        yield return new WaitForSeconds(duration);
        enemy.SetSpeed(ModiferHelper.UndoModifer(prevSpeed, value, modiferType));
        IsActive = false;
    }
    
}