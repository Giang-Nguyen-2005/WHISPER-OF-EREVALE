using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private string[] enemyTags; //Tag các loại enemy
    [SerializeField] private float spawnInterval = 2f;  // Cứ 2 giây đẻ 1 con
    [SerializeField] private float minDistance = 8f;   // Khoảng cách tối thiểu
    [SerializeField] private float maxDistance = 12f;  // Khoảng cách tối đa

    private Transform player;
    private float spawnTimer;

    void Start()
    {
        // Tìm Player để biết chỗ mà "bao vây"
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (player == null) return;

        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnInterval)
        {
            SpawnEnemy();
            spawnTimer = 0;
        }
    }

    private void SpawnEnemy()
    {
        int randomIndex = Random.Range(0, enemyTags.Length);
        string tagToSpawn = enemyTags[randomIndex];

        Vector2 spawnPos = GetRandomSpawnPosition();

        ObjectPooler.Instance.GetFromPool(tagToSpawn,spawnPos,Quaternion.identity);
    }

    private Vector2 GetRandomSpawnPosition()
    {
        // Lấy một hướng ngẫu nhiên
        float randomAngle = Random.Range(0f, Mathf.PI * 2f);
        
        // Lấy một khoảng cách ngẫu nhiên trong tầm min-max
        float randomDistance = Random.Range(minDistance, maxDistance);

        // Chuyển từ hệ tọa độ cực sang tọa độ phẳng (Vector2)
        float spawnX = player.position.x + Mathf.Cos(randomAngle) * randomDistance;
        float spawnY = player.position.y + Mathf.Sin(randomAngle) * randomDistance;

        return new Vector2(spawnX, spawnY);
    }

}