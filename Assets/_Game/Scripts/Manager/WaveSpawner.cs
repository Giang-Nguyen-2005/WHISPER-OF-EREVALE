using System.Collections;
using System.Collections.Generic;
using Mono.Cecil.Cil;
using Unity.Android.Gradle.Manifest;
using Unity.VisualScripting;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [Header("Wave Setting")]
    [SerializeField] private List<WaveData> waves;

    [SerializeField] private Transform[] spawnPoints;

    public Transform player;

    private bool isSpawning = false;
    private int currentWaveIndex = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        if (waves.Count > 0) StartCoroutine(StartNextWave());
    }
    IEnumerator StartNextWave()
    {
        isSpawning = true;
        WaveData currentWave = waves[currentWaveIndex];

        foreach (var group in currentWave.enemyGroups)
        {
            yield return StartCoroutine(SpawnEnemyGroup(group));
        }

        isSpawning = false;
        currentWaveIndex++;

        if (currentWaveIndex < waves.Count)
        {
            yield return new WaitForSeconds(currentWave.timeAfterWave);
            StartCoroutine(StartNextWave());
        }
    }
    IEnumerator SpawnEnemyGroup(EnemyGroup group)
    {
        for (int i = 0; i < group.enemyCount; i++)
        {
            // Lấy một hướng ngẫu nhiên
            float randomAngle = Random.Range(0f, Mathf.PI * 2f);

            // Lấy một khoảng cách ngẫu nhiên trong tầm min-max
            float randomDistance = Random.Range(10f, 12f);

            // Chuyển từ hệ tọa độ cực sang tọa độ phẳng (Vector2)
            float spawnX = player.position.x + Mathf.Cos(randomAngle) * randomDistance;
            float spawnY = player.position.y + Mathf.Sin(randomAngle) * randomDistance;


            ObjectPooler.Instance.GetFromPool(group.enemyTag, new Vector2(spawnX,spawnY), Quaternion.identity);

            yield return new WaitForSeconds(group.spawnInterval);
        }
    }



}
