using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeUI : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI descriptionText;

    private UpgradeData upgradeData;
    private LevelUpManager manager;

    // Hàm này được LevelUpManager gọi để đổ dữ liệu vào nút
    public void Setup(UpgradeData data, LevelUpManager levelUpManager)
    {
        upgradeData = data;
        manager = levelUpManager;

        // Cập nhật giao diện
        if (iconImage != null) iconImage.sprite = data.icon;
        nameText.text = data.upgradeName;
        descriptionText.text = data.description;
    }

    // Hàm này gán vào sự kiện OnClick của Button trong Inspector
    public void OnClickUpgrade()
    {
        if (upgradeData != null && manager != null)
        {
           manager.SelectUpgrade(upgradeData);
        }
    }
}