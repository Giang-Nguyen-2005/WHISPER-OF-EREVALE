using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ExperienceUI : MonoBehaviour
{
    [SerializeField] private Slider expSlider;
    [SerializeField] private TextMeshProUGUI levelText;
    private float targetProgress;
    
    private void OnEnable()
    {
        PlayerEvents.OnExperienceChanged += UpdateExpBar;
        PlayerEvents.OnLevelUp += UpdateLevelText;
    }

    private void OnDisable()
    {
        
        PlayerEvents.OnExperienceChanged -= UpdateExpBar;
        PlayerEvents.OnLevelUp -= UpdateLevelText;
    }

    private void UpdateExpBar(int currentExp, int maxExp)
    {
        targetProgress = (float)currentExp / maxExp;
    }

    private void UpdateLevelText(int currentLevel)
    {
        if (levelText != null)
        {
            levelText.text = "LV. " + currentLevel;
        }
    }
    void Update()
    {
        expSlider.value = Mathf.Lerp(expSlider.value, targetProgress, Time.deltaTime *5f);
    }

}
