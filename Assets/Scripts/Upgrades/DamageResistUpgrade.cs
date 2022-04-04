using System;
using UnityEngine;

[Serializable]
public class DamageResistUpgrade : Upgrade
{
    public ModiferType modiferType;
    [Range(0,1)]
    public float value;
    public override void ApplyUpgrade()
    {
        LevelManager.player.damageModifer = ModiferHelper.ApplyModifer(LevelManager.player.damageModifer, value, modiferType);
    }
}