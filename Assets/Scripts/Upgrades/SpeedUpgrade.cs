using System;

public enum SpeedUpgradeType
{
    ADD,
    MULTIPLER
}

[Serializable]
public class SpeedUpgrade : Upgrade
{
    public SpeedUpgradeType UpgradeType;
    public float value;
    public void ApplyUpgrade()
    {
        PlayerMovement playerMovement = LevelManager.player.GetComponent<PlayerMovement>();
        switch (UpgradeType)
        {
            case SpeedUpgradeType.ADD:
                playerMovement.moveSpeed += value;
                break;
            case SpeedUpgradeType.MULTIPLER:
                playerMovement.moveSpeed *= value;
                break;
        }
    }
}