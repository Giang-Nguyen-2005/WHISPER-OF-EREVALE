using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/Effects/Divine Smite")]
public class SmiteModifier : StatModifier
{
    [Header("Smite Settings")]
    public float cooldownReduction = 0f;
    public int bonusStrike = 0;        
    public bool unlockChain = false; 

    public override void Apply(PlayerManager player, float value)
    {
        SmiteController smite = player.GetComponent<SmiteController>();
        if (smite == null) return;

        if (!smite.isUnlocked) smite.isUnlocked = true;

        if (operation == StatOperation.Add)
        {
            smite.baseDamage += Mathf.RoundToInt(value);
            smite.cooldown += cooldownReduction;
            smite.strikeCount += bonusStrike;
            
            if (unlockChain) smite.isChainLightning = true;
        }
    }
}