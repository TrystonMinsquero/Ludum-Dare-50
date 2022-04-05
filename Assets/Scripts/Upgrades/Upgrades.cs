
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrades : MonoBehaviour
{
    public static Upgrades Instance;
    public Upgrade[] upgrades;

    [Header("Marks")]
    public SlowMark SlowMark;
    public StunMark StunMark;

    [Header("PlayerUpgrades")] 
    public SpeedUpgrade SpeedUpgrade;

    public TimeOnKillUpgrade TimeOnKillUpgrade;
    public DamageResistUpgrade DamageResistUpgrade;
    
    public static event Action<Upgrade> UpgradeApplied = delegate(Upgrade upgrade) {  };
    private static Upgrade[] _upgrades;


    private void Awake()
    {
        Populate();
        // StartCoroutine(Test(2));
    }

    private void Populate()
    {
        upgrades = new Upgrade[]
        {
            SlowMark,
            StunMark,
            SpeedUpgrade,
            TimeOnKillUpgrade,
            DamageResistUpgrade
        };
        _upgrades = upgrades;
    }

    private IEnumerator Test(float time)
    {
        yield return new WaitForSeconds(time);
        
        ApplyUpgrade("stun mark");
        ApplyUpgrade("slow mark");
    }

    public static void ApplyUpgrade(Upgrade upgrade)
    {
        upgrade.ApplyUpgrade();
        UpgradeApplied.Invoke(upgrade);
    }

    public static void ApplyUpgrade(string upgradeName)
    {
        foreach(Upgrade upgrade in _upgrades)
            if (upgradeName.ToLower() == upgrade.upgradeName.ToLower())
            {
                upgrade.ApplyUpgrade();
                UpgradeApplied.Invoke(upgrade);
                Debug.Log($"Applied {upgradeName}");
                return;
            }

        Debug.Log("upgrade name not found");
    }
    
}