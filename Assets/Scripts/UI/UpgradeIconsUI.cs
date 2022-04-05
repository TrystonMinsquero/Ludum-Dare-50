using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeIconsUI : MonoBehaviour
{
    public GameObject iconPrefab;
    public Transform startPos;
    public Text upgradeText;
    public Text upgradeDescriptionText;
    public int widthIncrement;
    public float descriptionShowTime = 5f;

    private int currentIndex = 0;

    private void Start()
    {
        upgradeText.enabled = false;
        upgradeDescriptionText.text = "";
    }

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
        upgradeText.enabled = true;
        // Debug.Log($"Add upgrade {upgrade.upgradeName}");
        Vector3 pos = startPos.position + Vector3.right * currentIndex * widthIncrement * 1f;
        var image = Instantiate(iconPrefab, transform).GetComponent<Image>();
        image.transform.position = pos;
        image.sprite = upgrade.icon;
        StartCoroutine(ShowUpgradeDescription(upgrade.upgradeDescription, descriptionShowTime));
        currentIndex++;
    }

    private IEnumerator ShowUpgradeDescription(string text, float time)
    {
        upgradeDescriptionText.text = text;
        yield return new WaitForSeconds(time);
        upgradeDescriptionText.text = "";
    }
}
