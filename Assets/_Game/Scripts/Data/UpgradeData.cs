using UnityEngine;

[CreateAssetMenu(fileName = "NewUpgrade", menuName = "Combat/UpgradeData")]
public class UpgradeData : ScriptableObject
{
    public string upgradeName;
    [TextArea] public string description;
    public Sprite icon;
    public StatModifier modifier;
    public float value;
}
