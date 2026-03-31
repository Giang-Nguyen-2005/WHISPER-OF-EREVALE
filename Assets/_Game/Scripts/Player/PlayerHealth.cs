using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [Header("Settings")]
    public PlayerData data;
    private int currentHealth;
    private bool isDead = false;
    private bool isInvincible = false;

    private PlayerManager player;

    public bool IsDead => isDead;

    void Start()
    {
        player = GetComponent<PlayerManager>();
        currentHealth = data.maxHealth; 
    }

    public void TakeDamage(int damage)
    {
        if (isDead || isInvincible) return;

        currentHealth -= damage;
        
        // Rung màn hình hoặc Flash màu trắng (Juice)
        StartCoroutine(HitFlashRoutine());
        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            // Bắt đầu thời gian bất tử để không bị "bốc hơi" quá nhanh
            StartCoroutine(InvincibilityRoutine());
        }
    }

    private IEnumerator InvincibilityRoutine()
    {
        isInvincible = true;
        // Giang có thể làm nhân vật nhấp nháy ở đây
        yield return new WaitForSeconds(data.invincibilityDuration);
        isInvincible = false;
    }

    private IEnumerator HitFlashRoutine()
    {
        // Đổi màu Sprite sang đỏ nhạt hoặc trắng để báo hiệu trúng đòn
        player.anim.GetComponentInChildren<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.1f);
        player.anim.GetComponentInChildren<SpriteRenderer>().color = Color.white;
    }

    private void Die()
    {
        if (isDead) return;
        isDead = true;
        player.anim.TriggerDeath();
        Debug.Log("Chết");
        // Ngừng di chuyển
        player.rb.linearVelocity = Vector2.zero;
        this.enabled = false;
    }
}