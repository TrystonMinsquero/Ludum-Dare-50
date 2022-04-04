
using System;
using UnityEngine;

public enum upgradeType
{
    Mark,
    Player
}

[Serializable][CreateAssetMenu(fileName = "Upgrade")]
public class UpgradeObject : ScriptableObject
{
    public string upgradeName;
    public Upgrade upgrade;
    public Sprite image;

}

public class Upgrades : MonoBehaviour
{
    public static Upgrades Instance;
    public Upgrade[] upgrades;

    [Header("Marks")]
    public BasicMark BasicMark;
    public SlowMark SlowMark;
    public StunMark StunMark;

    [Header("PlayerUpgrades")] 
    public SpeedUpgrade SpeedUpgrade;

    private void Populate()
    {
        upgrades = new Upgrade[]
        {
            BasicMark,
            SlowMark,
            StunMark,
            SpeedUpgrade
        };
    }
    
}