
using System.Collections;
using UnityEngine;

[System.Serializable]
public class StunMark : Mark
{
    public override IEnumerator ApplyMark(Enemy enemy)
    {
        enemy.Stun();
        IsActive = true;
        yield return new WaitForSeconds(duration);
        enemy.UnStun();
        IsActive = false;
    }
}