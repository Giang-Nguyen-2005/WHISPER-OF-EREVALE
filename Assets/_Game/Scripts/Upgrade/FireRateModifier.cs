using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/Effects/Fire Rate")]
public class FireRateModifier : StatModifier
{
    public override void Apply(PlayerManager player, float value)
    {
        player.combat.bonusFireRate -= value;
    }
}