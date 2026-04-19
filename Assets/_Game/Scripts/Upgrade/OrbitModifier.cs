using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/Effects/Orbiting Fireball")]
public class OrbitModifier : StatModifier
{
    public override void Apply(PlayerManager player, float value)
    {
        OrbitController orbit = player.GetComponent<OrbitController>();
        
        if (orbit != null && operation == StatOperation.Add)
        {
            orbit.AddFireball(Mathf.RoundToInt(value));
        }
    }
}