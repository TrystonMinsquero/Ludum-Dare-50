using System;
[Serializable]
public class DamageResistUpgrade : Upgrade
{
    public ModiferType UpgradeType;
    public float value;
    public override void ApplyUpgrade()
    {
        switch (UpgradeType)
        {
            case ModiferType.ADD:
                LevelManager.player.damageModifer += value;
                break;
            case ModiferType.MULTIPLER:
                LevelManager.player.damageModifer *= value;
                break;
        }
    }
}