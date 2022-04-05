using System.Collections;
using UnityEngine;

[System.Serializable]
public abstract class Mark : Upgrade
{
    public float duration;
    public bool IsActive { get; protected set; }

    public Mark(float duration, string upgradeName)
    {
        this.duration = duration;
        this.upgradeName = upgradeName;
    }
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
        IsActive = true;
        yield return new WaitForSeconds(duration);
        IsActive = false;
    }

    public override void ApplyUpgrade()
    {
        LevelManager.weapon.marks.Add(this);
    }
}
