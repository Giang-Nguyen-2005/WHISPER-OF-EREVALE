using UnityEngine;

public abstract class StatModifier : ScriptableObject
{
    public abstract void Apply(PlayerManager player, float value);
}

