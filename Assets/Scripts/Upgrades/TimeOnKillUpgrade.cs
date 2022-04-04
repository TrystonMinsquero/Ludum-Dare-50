[System.Serializable]
public class TimeOnKillUpgrade : Upgrade
{
    public float value;
    public override void ApplyUpgrade()
    {
        LevelManager.player.timeOnKill = value;
    }
}