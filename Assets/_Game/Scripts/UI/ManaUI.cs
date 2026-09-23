using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ManaUI : MonoBehaviour
{
    [Header("UI Components")]
    [SerializeField] private Slider manaSlider;
    [SerializeField] private Image fillImage; // Để đổi màu khi đầy mana
    [SerializeField] private TextMeshProUGUI manaText;

    [Header("Settings")]
    [SerializeField] private Color normalColor = Color.cyan;
    [SerializeField] private Color fullColor = Color.yellow;
    [SerializeField] private float lerpSpeed = 5f;

    private float targetProgress;

    private void OnEnable()
    {
        // ĐÂY LÀ CHỖ "LẮNG NGHE": Khi sự kiện OnManaChanged phát ra, hàm UpdateManaUI sẽ tự chạy
        PlayerEvents.OnManaChanged += UpdateManaUI;
    }

    private void OnDisable()
    {
        // Hủy lắng nghe khi UI bị đóng để tránh lỗi bộ nhớ
        PlayerEvents.OnManaChanged -= UpdateManaUI;
    }

    private void UpdateManaUI(int current, int max)
    {
        targetProgress = (float)current / max;

        if (manaText != null)
        {
            manaText.text = $"{current} / {max}";
        }

        // Đổi màu thanh Mana nếu đầy
        if (fillImage != null)
        {
            fillImage.color = (current >= max) ? fullColor : normalColor;
        }
    }

    private void Update()
    {
        // Giúp thanh Slider chạy mượt mà thay vì khựng một phát
        manaSlider.value = Mathf.Lerp(manaSlider.value, targetProgress, Time.deltaTime * lerpSpeed);
        
        // Hiệu ứng nhấp nháy khi đầy Mana (Vip Pro)
        if (targetProgress >= 1f)
        {
            float pingPong = Mathf.PingPong(Time.time * 2f, 1f);
            fillImage.color = Color.Lerp(fullColor, Color.white, pingPong);
        }
    }
}