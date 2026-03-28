using UnityEngine;

[CreateAssetMenu(fileName ="NewEnemyData", menuName= "Combat/EnemyData")]
public class EnemyData : ActorData
{
    [Header("Combat Stats")]
    public float attackRange = 1.5f;
    public float attackCooldown = 1.5f;
    public int contactDamage = 10;
}