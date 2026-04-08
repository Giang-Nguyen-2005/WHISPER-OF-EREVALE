using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/Effects/Move Speed")]
public class MoveSpeedModifier : StatModifier
{
    public override void Apply(PlayerManager player, float value)
    {
        player.movement.bonusSpeed += value;
    }
}
