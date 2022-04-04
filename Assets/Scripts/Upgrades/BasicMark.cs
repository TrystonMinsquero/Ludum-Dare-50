using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class BasicMark : Mark
{
    public override IEnumerator ApplyMark(Enemy enemy)
    {
        isActive = true;
        yield return new WaitForSeconds(duration);
        isActive = false;
    }
}
