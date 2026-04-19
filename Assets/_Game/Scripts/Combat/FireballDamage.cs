using System.Collections.Generic;
using UnityEngine;

public class FireballDamage : MonoBehaviour
{
    [Header("Damage Settings")]
    public int baseDamage = 15; 
    public float hitCooldown = 0.5f;
    private PlayerManager player;
    private Dictionary<Collider2D, float> lastHitTimes = new Dictionary<Collider2D, float>();

    public void Setup(PlayerManager _player)
    {
        player = _player;
    }

    // Dùng Stay vì cầu lửa bay xuyên qua và có thể nằm trên người quái nhiều frame
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.TryGetComponent(out IDamageable hitTarget))
        {
            if (other.CompareTag("Player")) return;

            if (lastHitTimes.TryGetValue(other, out float lastHitTime))
            {
                if (Time.time < lastHitTime + hitCooldown) return;
            }

            int finalDamage = baseDamage;
            if (player != null && player.combat != null)
            {
                finalDamage = Mathf.RoundToInt((baseDamage + player.combat.bonusDamage));
            }

            // 4. Đốt cháy quái!
            hitTarget.TakeDamage(finalDamage);

            // 5. Ghi lại thời gian vừa đốt
            lastHitTimes[other] = Time.time;
        }
    }

    // Dọn dẹp bộ nhớ khi quái đi ra khỏi cầu lửa (hoặc chết)
    private void OnTriggerExit2D(Collider2D other)
    {
        if (lastHitTimes.ContainsKey(other))
        {
            lastHitTimes.Remove(other);
        }
    }
}