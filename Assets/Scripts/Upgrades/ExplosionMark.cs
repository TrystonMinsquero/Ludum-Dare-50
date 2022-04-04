
using System.Collections;

[System.Serializable]
public class ExplosionMark : Mark
{
    public override IEnumerator ApplyMark(Enemy enemy)
    {
        yield return null;
    }
}