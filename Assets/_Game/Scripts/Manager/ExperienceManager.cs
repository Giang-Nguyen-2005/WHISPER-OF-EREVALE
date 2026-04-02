using System;
using Unity.VisualScripting;
using UnityEngine;

public class ExperienceManager : MonoBehaviour
{
    public static ExperienceManager Instance;

    [Header("Level Setting")]
    public int currentLevel = 1;
    public int currentExperience = 0;
    public int experienceToNextLevel = 100;

    [Header("Settings")]
    [SerializeField] private int baseExperience = 100;
    [SerializeField] private float multiplier = 1.2f;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerEvents.OnExperienceChanged?.Invoke(currentExperience, experienceToNextLevel);
        PlayerEvents.OnLevelUp?.Invoke(currentLevel);
    }
    public void AddExperience(int amount)
    {
        currentExperience += amount;
        while (currentExperience >= experienceToNextLevel)
        {
            LevelUp();
        }
        PlayerEvents.OnExperienceChanged?.Invoke(currentExperience, experienceToNextLevel);
    }
    public void LevelUp()
    {
        currentLevel++;
        currentExperience -= experienceToNextLevel;
        experienceToNextLevel = Mathf.RoundToInt(baseExperience * Mathf.Pow(currentLevel, multiplier));
        PlayerEvents.OnLevelUp?.Invoke(currentLevel);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
