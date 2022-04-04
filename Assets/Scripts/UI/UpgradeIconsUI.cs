using UnityEngine;
using UnityEngine.UI;

public class UpgradeIconsUI : MonoBehaviour
{
    public GameObject iconPrefab;
    public Transform startPos;
    public int widthIncrement;

    private int currentIndex = 0;

    private void OnEnable()
    {
        Upgrades.UpgradeApplied += AddUpgrade;
    }

    private void OnDisable()
    {
        
        Upgrades.UpgradeApplied -= AddUpgrade;
    }

    public void AddUpgrade(Upgrade upgrade)
    {
        // Debug.Log($"Add upgrade {upgrade.upgradeName}");
        Vector3 pos = startPos.position + Vector3.right * currentIndex * widthIncrement * 2;
        var image = Instantiate(iconPrefab, transform).GetComponent<Image>();
        image.transform.position = pos;
        image.sprite = upgrade.icon;
        currentIndex++;
    }
}