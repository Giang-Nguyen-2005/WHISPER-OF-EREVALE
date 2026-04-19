using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/Effects/Damage")]
public class DamageModifier : StatModifier
{
    public override void Apply(PlayerManager player, float value)
    {
        if (operation == StatOperation.Add)
        {
            player.combat.bonusDamage += Mathf.RoundToInt(value);
        }
        else if (operation == StatOperation.Multiply)
        {
            player.combat.bonusDamage = Mathf.RoundToInt(player.combat.bonusDamage * value);
        }
    }
}