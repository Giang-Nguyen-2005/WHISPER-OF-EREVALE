using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeUI : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private TextMeshProUGUI levelBadgeText; // Thêm ô text hiển thị Level

    private UpgradeData upgradeData;
    private LevelUpManager manager;

    // Cập nhật hàm Setup để nhận thêm tham số level
    public void Setup(UpgradeData data, LevelUpManager levelUpManager, int nextLevel)
    {
        upgradeData = data;
        manager = levelUpManager;

        if (iconImage != null) iconImage.sprite = data.icon;
        nameText.text = data.upgradeName;
        
        // Hiển thị mô tả tương ứng với Level tiếp theo
        descriptionText.text = data.GetDescriptionForLevel(nextLevel);
        
        if (levelBadgeText != null) levelBadgeText.text = "Lv." + nextLevel;
    }

    public void OnClickUpgrade()
    {
        if (upgradeData != null && manager != null) manager.SelectUpgrade(upgradeData);
    }
}