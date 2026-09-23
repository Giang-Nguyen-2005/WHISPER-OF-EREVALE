using System.Collections;
using UnityEngine;

public class UltimateController : MonoBehaviour
{
    [Header("Mana Settings")]
    public int maxMana = 100;
    public int currentMana = 0;

    [Header("Awakening Settings")]
    public float duration = 10f;
    public float speedBoost = 2f;
    public GameObject specialGunObject; // Sprite súng gắn trên Player
    public string specialBulletTag = "SpecialBullet"; // Tag trong Pooler
    public float fireRate = 0.1f; // Tốc độ sấy cực nhanh

    [Header("VFX")]
    public GameObject screenVFX;

    public bool isUltimateActive = false;
    private PlayerManager player;
    private WeaponBase savedWeapon;
    private float fireTimer;

    void Start()
    {
        player = GetComponent<PlayerManager>();
        if (specialGunObject != null) specialGunObject.SetActive(false);
        PlayerEvents.OnManaChanged?.Invoke(currentMana, maxMana);
    }

    void Update()
    {
        // Kích hoạt bằng phím R
        if (Input.GetKeyDown(KeyCode.R) && currentMana >= maxMana && !isUltimateActive)
        {
            StartCoroutine(AwakeningRoutine());
        }

        // Logic tự động bắn đạn đặc biệt khi đang Awakening
        if (isUltimateActive)
        {
            HandleSpecialFire();
        }
    }

    private void HandleSpecialFire()
    {
        fireTimer -= Time.deltaTime;
        if (fireTimer <= 0)
        {
            ShootSpecialBullet();
            fireTimer = fireRate;
        }
    }

    private void ShootSpecialBullet()
    {
        // 1. Tính toán hướng và góc bắn
        Vector2 dir = player.movement.lastDirection;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        
        // 2. Dịch điểm bắn ra xa Player một chút (để không kẹt trong bụng)
        Vector3 spawnPos = transform.position + (Vector3)dir * 0.5f;

        // 3. Đẻ đạn ra
        GameObject bulletObj = ObjectPooler.Instance.GetFromPool(specialBulletTag, spawnPos, Quaternion.Euler(0, 0, angle));

        // 4. CHỖ NÀY QUAN TRỌNG: Setup sát thương và chặn bắn trúng phe mình
        if (bulletObj.TryGetComponent(out Bullet bulletScript))
        {
            // Lấy Layer của Player để viên đạn biết đường mà né
            LayerMask playerLayer = LayerMask.GetMask("Player"); 
            
            // Init(Sát thương, Tốc bay, Layer bỏ qua, Thời gian sống)
            // Sát thương 50, bay tốc độ 15, né Player, sống 2 giây
            bulletScript.Init(50f, 15f, playerLayer, 2f); 
        }
    }

    private IEnumerator AwakeningRoutine()
    {
        isUltimateActive = true;
        currentMana = 0;
        PlayerEvents.OnManaChanged?.Invoke(currentMana, maxMana);

        // 1. Lưu vũ khí cũ và ép về Tay không (ID 0) [cite: 328]
        savedWeapon = player.combat.currentWeapon;
        player.combat.SwitchWeapon(null);

        // 2. Kích hoạt hình ảnh súng đặc biệt và buff tốc độ [cite: 362]
        if (specialGunObject != null) specialGunObject.SetActive(true);
        if (screenVFX != null) screenVFX.SetActive(true);
        player.movement.bonusSpeed += speedBoost;

        yield return new WaitForSeconds(duration);

        // 3. Kết thúc trạng thái
        player.movement.bonusSpeed -= speedBoost;
        if (specialGunObject != null) specialGunObject.SetActive(false);
        if (screenVFX != null) screenVFX.SetActive(false);

        // Trả lại vũ khí cũ
        player.combat.SwitchWeapon(savedWeapon);
        isUltimateActive = false;
    }

    public void AddMana(int amount)
    {
        if (isUltimateActive) return;
        currentMana = Mathf.Min(currentMana + amount, maxMana);
        PlayerEvents.OnManaChanged?.Invoke(currentMana, maxMana);
    }
}