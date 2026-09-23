using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    [Header("Map Settings")]
    [Tooltip("Kéo cái Prefab MapChunk của ông vào đây")]
    public GameObject chunkPrefab;   
    
    [Tooltip("Kích thước thực tế của 1 MapChunk. Nếu ông vẽ 32x32 thì điền 32")]
    public float chunkSize = 32f;    
    
    [Tooltip("Kéo Player của ông vào đây để hệ thống lấy tọa độ")]
    public Transform player;         

    // Lưu trữ các mảnh map đang hiện trên màn hình (để dễ quản lý)
    private Dictionary<Vector2, GameObject> activeChunks = new Dictionary<Vector2, GameObject>();
    
    // Kho chứa đồ tái chế (Object Pool). Thay vì xóa map cũ đi thì cất vào đây dùng lại cho nhẹ máy
    private Queue<GameObject> chunkPool = new Queue<GameObject>();

    // Ghi nhớ xem Player đang đứng ở cái ô tọa độ (X, Y) nào
    private Vector2 currentChunkPosition = new Vector2(0, 0); 

    void Start()
    {
        // Vừa vào game là đẻ ngay 9 cái map (3x3) bao quanh Player
        UpdateMapChunks();
    }

    void Update()
    {
        // 1. Dò xem Player hiện tại đang dẫm lên cái MapChunk có tọa độ bao nhiêu
        int currentX = Mathf.RoundToInt(player.position.x / chunkSize);
        int currentY = Mathf.RoundToInt(player.position.y / chunkSize);
        Vector2 newChunkPosition = new Vector2(currentX, currentY);

        // 2. Nếu Player CHẠY SANG Ô MỚI -> Kích hoạt cơ chế cuộn thảm!
        if (currentChunkPosition != newChunkPosition)
        {
            currentChunkPosition = newChunkPosition;
            UpdateMapChunks(); // Gọi hàm cập nhật map
        }
    }

    // Hàm não bộ: Xử lý đẻ map mới và cất map cũ
    private void UpdateMapChunks()
    {
        // Danh sách 9 vị trí MỚI xung quanh Player
        List<Vector2> newActivePositions = new List<Vector2>();

        // Vòng lặp quét 3x3 (X từ -1 đến 1, Y từ -1 đến 1)
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                // Tọa độ của 1 ô trong lưới 3x3
                Vector2 targetPos = new Vector2(currentChunkPosition.x + x, currentChunkPosition.y + y);
                newActivePositions.Add(targetPos);

                // Nếu tại ô này CHƯA CÓ MAP -> Kêu hàm SpawnChunk đẻ map ra
                if (!activeChunks.ContainsKey(targetPos))
                {
                    SpawnChunk(targetPos);
                }
            }
        }

        // --- DỌN RÁC (CUỘN THẢM) ---
        // Quét lại những map ĐANG HIỆN, nếu cái nào nằm ngoài vùng 3x3 mới -> Cất vào kho
        List<Vector2> chunksToRemove = new List<Vector2>();
        
        foreach (var chunk in activeChunks)
        {
            // Nếu vị trí của map cũ KHÔNG nằm trong danh sách 9 vị trí mới
            if (!newActivePositions.Contains(chunk.Key))
            {
                chunk.Value.SetActive(false); // Tắt nó đi (Cất vào kho)
                chunkPool.Enqueue(chunk.Value); // Đẩy vào Queue để tái sử dụng
                chunksToRemove.Add(chunk.Key); // Đưa vào danh sách chờ xóa khỏi Dictionary
            }
        }

        // Xóa tên các map đã cất khỏi danh sách "đang hoạt động"
        foreach (var pos in chunksToRemove)
        {
            activeChunks.Remove(pos);
        }
    }

    // Hàm thợ xây: Đặt 1 MapChunk vào đúng tọa độ
    private void SpawnChunk(Vector2 gridPosition)
    {
        GameObject chunk;
        
        // Tính toán tọa độ thực tế trong không gian Unity (Lấy tọa độ lưới nhân với kích thước)
        Vector3 spawnPosition = new Vector3(gridPosition.x * chunkSize, gridPosition.y * chunkSize, 0);

        // Kiểm tra xem trong kho (Pool) có map nào đang rảnh không?
        if (chunkPool.Count > 0)
        {
            // CÓ -> Lấy ra dùng luôn, đỡ phải đẻ mới (Tiết kiệm CPU)
            chunk = chunkPool.Dequeue();
            chunk.transform.position = spawnPosition;
            chunk.SetActive(true);
        }
        else 
        {
            // KHÔNG CÓ -> Bắt buộc phải đẻ map mới
            chunk = Instantiate(chunkPrefab, spawnPosition, Quaternion.identity);
        }

        // Ghi danh nó vào sổ "Các map đang hoạt động"
        activeChunks.Add(gridPosition, chunk);
    }
}