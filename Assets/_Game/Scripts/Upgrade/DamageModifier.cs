using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/Effects/Damage")]
public class DamageModifier : StatModifier
{
    public override void Apply(PlayerManager player, float value)
    {
        player.combat.bonusDamage += Mathf.RoundToInt(value);
    }
}