using System;
[Serializable]
public class SpeedUpgrade : Upgrade
{
    public ModiferType modiferType;
    public float value;
    public override void ApplyUpgrade()
    {
        PlayerMovement playerMovement = LevelManager.player.GetComponent<PlayerMovement>();
        playerMovement.moveSpeed = ModiferHelper.ApplyModifer(playerMovement.moveSpeed, value, modiferType);
    }
}