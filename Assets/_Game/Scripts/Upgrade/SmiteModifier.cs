using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/Effects/Divine Smite")]
public class SmiteModifier : StatModifier
{
    [Header("Smite Level Specific Changes")]
    public int damageIncrease = 0;
    public float cooldownReduction = 0f;
    public int strikeIncrease = 0; 
    public bool unlockChainLightning = false;

    public override void Apply(PlayerManager player, float value)
    {
        SmiteController smite = player.GetComponent<SmiteController>();
        if (smite == null) return;

        if (!smite.isUnlocked) smite.isUnlocked = true;
    
        smite.baseDamage += damageIncrease;
        
        smite.cooldown -= cooldownReduction; 
        
        smite.strikeCount += strikeIncrease;
        
        if (unlockChainLightning) smite.isChainLightning = true;
    }
}