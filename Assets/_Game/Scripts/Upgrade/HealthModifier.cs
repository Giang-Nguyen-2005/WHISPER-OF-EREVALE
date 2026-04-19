using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/Effects/Health")]
public class HealthModifier : StatModifier
{
    public override void Apply(PlayerManager player, float value)
    {
        PlayerHealth health = player.GetComponent<PlayerHealth>();
        if (health != null)
        {
            health.UpdateMaxHealth(value, operation);
        }
    }
}