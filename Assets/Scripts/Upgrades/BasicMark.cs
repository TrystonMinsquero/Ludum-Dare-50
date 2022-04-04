using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class BasicMark : Mark
{
    public override IEnumerator ApplyMark(Enemy enemy)
    {
        IsActive = true;
        yield return new WaitForSeconds(duration);
        IsActive = false;
    }
}
