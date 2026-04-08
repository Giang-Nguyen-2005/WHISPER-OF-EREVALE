using UnityEngine;

[CreateAssetMenu(fileName = "NewRangedEnemyData", menuName = "Combat/RangedEnemyData")]
public class RangedEnemyData : EnemyData
{
    public float stopDistance = 5f;
    public string bulletTag;
    public float bulletSpeed = 5f;
    public float bulletLifeTime = 6f;
}