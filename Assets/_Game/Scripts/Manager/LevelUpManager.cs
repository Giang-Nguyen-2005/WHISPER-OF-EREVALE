using System.Collections.Generic;
using NUnit.Framework.Interfaces;
using Unity.VisualScripting;
using UnityEngine;

public class LevelUpManager : MonoBehaviour
{
    public PlayerManager player;

    [Header("UI Reference")]
    [SerializeField] private GameObject levelUpPanel;
    [SerializeField] private UpgradeUI[] upgradeButtons;

    [Header("Data")]
    [SerializeField] private List<UpgradeData> allUpgrades;

    void OnEnable()
    {
        PlayerEvents.OnLevelUp += ShowLevelUpUI;
    }
    void OnDisable()
    {
        PlayerEvents.OnLevelUp -= ShowLevelUpUI;
    }
    private void ShowLevelUpUI(int currentLevel)
    {
        Time.timeScale = 0;
        levelUpPanel.SetActive(true);

        List<UpgradeData> selectedUpgrades = GetRandomUpgrades(upgradeButtons.Length);

        for (int i = 0; i < upgradeButtons.Length; i++)
        {
            upgradeButtons[i].Setup(selectedUpgrades[i], this);
        }
    }
    private List<UpgradeData> GetRandomUpgrades(int count)
    {
        List<UpgradeData> list = new List<UpgradeData>(allUpgrades);
        List<UpgradeData> result = new List<UpgradeData>();

        for (int i = 0; i < count && list.Count > 0; i++)
        {
            int index = Random.Range(0, list.Count);
            result.Add(list[index]);
            list.RemoveAt(index);
        }
        return result;
    }
    public void SelectUpgrade(UpgradeData data)
    {
        ApplyUpgradeEffect(data);
        levelUpPanel.SetActive(false);
        Time.timeScale = 1;
    }
    private void ApplyUpgradeEffect(UpgradeData data)
    {
        switch (data.type)
        {
            case UpgradeData.UpgradeType.MoveSpeed:
                player.movement.bonusSpeed += data.value;
                break;
            case UpgradeData.UpgradeType.FireRate:
                player.combat.bonusFireRate -= data.value;
                break;
            case UpgradeData.UpgradeType.Damage:
                player.combat.bonusDamage += Mathf.RoundToInt(data.value);
                break;
        }
    }
}
