using UnityEngine;

public abstract class StatModifier : ScriptableObject
{
    public enum StatOperation { Add, Multiply}
    public StatOperation operation;

    public abstract void Apply(PlayerManager player, float value);

    protected float Calculate(float baseValue, float upgradeValue)
    {
        return operation == StatOperation.Add ? (baseValue + upgradeValue) : (baseValue * upgradeValue);
    }
}