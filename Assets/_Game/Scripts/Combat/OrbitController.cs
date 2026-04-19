using System.Collections.Generic;
using UnityEngine;

public class OrbitController : MonoBehaviour
{
    [Header("Orbit Settings")]
    public GameObject fireballPrefab;
    public float orbitRadius = 2f;    // Khoảng cách từ Player đến cầu lửa
    public float orbitSpeed = 100f; 

    [Header("Current Stats")]
    public int currentFireballCount = 0;
    private List<GameObject> activeFireballs = new List<GameObject>(); 

    private void Update()
    {
        // Nếu không có quả nào thì không làm gì cả
        if (currentFireballCount <= 0 || activeFireballs.Count == 0) return;

        float angleStep = 360f / currentFireballCount; // Chia đều khoảng cách góc giữa các quả

        for (int i = 0; i < activeFireballs.Count; i++)
        {
            if (activeFireballs[i] != null)
            {
                // Tính toán góc hiện tại dựa trên thời gian
                float currentAngle = (Time.time * orbitSpeed) + (angleStep * i);
                
                // Chuyển góc thành tọa độ X, Y
                float rad = currentAngle * Mathf.Deg2Rad;
                float x = transform.position.x + Mathf.Cos(rad) * orbitRadius;
                float y = transform.position.y + Mathf.Sin(rad) * orbitRadius;

                // Cập nhật vị trí
                activeFireballs[i].transform.position = new Vector3(x, y, 0);
            }
        }
    }
    public void AddFireball(int amount)
    {
        currentFireballCount += amount;

        for (int i = 0; i < amount; i++)
        {
            GameObject newFireball = Instantiate(fireballPrefab, transform.position, Quaternion.identity, transform);
            if (newFireball.TryGetComponent(out FireballDamage fd))
            {
                fd.Setup(GetComponent<PlayerManager>());
            }
            activeFireballs.Add(newFireball);
        }
    }
}