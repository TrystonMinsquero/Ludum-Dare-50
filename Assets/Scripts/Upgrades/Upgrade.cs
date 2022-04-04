using UnityEngine;

[System.Serializable]
public abstract class Upgrade
{
    public string upgradeName;
    [TextArea(1,3)]
    public string upgradeDescription;
    public Sprite icon;
    public abstract void ApplyUpgrade();
}