using System;
using System.Collections;
using UnityEngine;

[Serializable]
public abstract class Mark
{
    public string name;
    public float duration;
    public abstract IEnumerator ApplyMark(Enemy enemy);
}
