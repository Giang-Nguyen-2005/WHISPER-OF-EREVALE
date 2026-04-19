using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/Effects/Move Speed")]
public class MoveSpeedModifier : StatModifier
{
    public override void Apply(PlayerManager player, float value)
    {
        if (operation == StatOperation.Add)
        {
            player.movement.bonusSpeed += value;
        }
        else if (operation == StatOperation.Multiply)
        {
            float currentTotalSpeed = player.movement.data.walkSpeed + player.movement.bonusSpeed;
            player.movement.bonusSpeed = (currentTotalSpeed * value) - player.movement.data.walkSpeed;
        }
    }
}