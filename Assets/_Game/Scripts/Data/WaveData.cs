using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyGroup
{
    public string enemyTag;
    public int enemyCount;
    public float spawnInterval;// khoang cach giua enemy
}

[CreateAssetMenu(fileName = "NewWave", menuName = "Combat/WaveData")]
public class WaveData : ScriptableObject
{
    public List<EnemyGroup> enemyGroups; 
    public float timeAfterWave = 5f;     // Time cho Wave tiep theo
}