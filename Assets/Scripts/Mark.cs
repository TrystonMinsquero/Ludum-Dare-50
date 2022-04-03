using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class Mark
{
    public string name;
    public float duration;
    public bool isActive;

    public Mark(float duration)
    {
        this.duration = duration;
    }
    public Mark()
    {
        this.duration = 3f;
    }


    public virtual IEnumerator ApplyMark(Enemy enemy)
    {
        isActive = true;
        yield return new WaitForSeconds(duration);
        isActive = false;
    }
}
