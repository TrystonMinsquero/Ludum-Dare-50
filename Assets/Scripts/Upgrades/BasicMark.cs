using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class BasicMark : Mark
{
    public BasicMark(float duration, string upgradeName)
    {
        this.duration = duration;
        this.upgradeName = upgradeName;
    }
    
    public override IEnumerator ApplyMark(Enemy enemy)
    {
        IsActive = true;
        yield return new WaitForSeconds(duration);
        IsActive = false;
    }
}
