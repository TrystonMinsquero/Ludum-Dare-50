﻿using System;
[Serializable]
public class SpeedUpgrade : Upgrade
{
    public ModiferType UpgradeType;
    public float value;
    public override void ApplyUpgrade()
    {
        PlayerMovement playerMovement = LevelManager.player.GetComponent<PlayerMovement>();
        switch (UpgradeType)
        {
            case ModiferType.ADD:
                playerMovement.moveSpeed += value;
                break;
            case ModiferType.MULTIPLER:
                playerMovement.moveSpeed *= value;
                break;
        }
    }
}