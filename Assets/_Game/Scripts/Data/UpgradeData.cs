using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NewUpgrade", menuName = "Combat/UpgradeData")]
public class UpgradeData : ScriptableObject
{
    public string upgradeID;
    public string upgradeName;
    public Sprite icon;
    public StatModifier modifier;
    [Header("Level Settings")]
    public int maxLevel=5;
    public float[] valuesPerLevel =new float[5];

    [TextArea] public string[] descriptionsPerLevel = new string[5];

    public float GetValueForLevel(int level)
    {
        int i =Mathf.Clamp(level-1,0,maxLevel-1);
        return valuesPerLevel[i];
    }
    public string GetDescriptionForLevel(int level)
    {
        int i =Mathf.Clamp(level-1,0,maxLevel-1);
        return descriptionsPerLevel[i];
    }
}
