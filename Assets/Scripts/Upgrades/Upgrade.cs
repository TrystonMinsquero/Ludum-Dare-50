using UnityEngine;

[System.Serializable]
public abstract class Upgrade
{
    public string upgradeName;
    public Sprite icon;
    public abstract void ApplyUpgrade();
}