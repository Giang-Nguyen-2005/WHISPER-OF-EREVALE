using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/Effects/Fire Rate")]
public class FireRateModifier : StatModifier
{
    public override void Apply(PlayerManager player, float value)
    {
        if (operation == StatOperation.Add)
        {
            player.combat.bonusFireRate += value;
        }
        else if (operation == StatOperation.Multiply)
        {
            player.combat.bonusFireRate *= value;
        }
    }
}