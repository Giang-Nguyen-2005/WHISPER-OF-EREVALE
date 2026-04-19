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
    private Dictionary<string, int> playerUpgradeLevels = new Dictionary<string, int>();

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

        // Lấy danh sách các nâng cấp chưa Max Level
        List<UpgradeData> availablePool = allUpgrades.FindAll(u =>
            !playerUpgradeLevels.ContainsKey(u.upgradeID) ||
            playerUpgradeLevels[u.upgradeID] < u.maxLevel);

        List<UpgradeData> selectedUpgrades = GetRandomUpgradesFromPool(availablePool, upgradeButtons.Length);

        for (int i = 0; i < upgradeButtons.Length; i++)
        {
            if (i < selectedUpgrades.Count)
            {
                upgradeButtons[i].gameObject.SetActive(true);
                // Gửi thêm thông tin Level tiếp theo để UI hiển thị
                int nextLevel = GetNextLevel(selectedUpgrades[i]);
                upgradeButtons[i].Setup(selectedUpgrades[i], this, nextLevel);
            }
            else upgradeButtons[i].gameObject.SetActive(false);
        }
    }
    private int GetNextLevel(UpgradeData data)
    {
        if (!playerUpgradeLevels.ContainsKey(data.upgradeID)) return 1;
        return playerUpgradeLevels[data.upgradeID] + 1;
    }

    private List<UpgradeData> GetRandomUpgradesFromPool(List<UpgradeData> pool, int count)
    {
        List<UpgradeData> result = new List<UpgradeData>();
        List<UpgradeData> tempPool = new List<UpgradeData>(pool);

        for (int i = 0; i < count && tempPool.Count > 0; i++)
        {
            int index = Random.Range(0, tempPool.Count);
            result.Add(tempPool[index]);
            tempPool.RemoveAt(index);
        }
        return result;
    }

    public void SelectUpgrade(UpgradeData data)
    {
        // Tăng cấp độ trong Dictionary
        if (!playerUpgradeLevels.ContainsKey(data.upgradeID)) playerUpgradeLevels.Add(data.upgradeID, 1);
        else playerUpgradeLevels[data.upgradeID]++;

        ApplyUpgradeEffect(data);

        levelUpPanel.SetActive(false);
        Time.timeScale = 1;
    }

    private void ApplyUpgradeEffect(UpgradeData data)
    {
        if (data.modifier != null)
        {
            int currentLv = playerUpgradeLevels[data.upgradeID];
            float valueToApply = data.GetValueForLevel(currentLv);
            data.modifier.Apply(player, valueToApply);
        }
    }
}