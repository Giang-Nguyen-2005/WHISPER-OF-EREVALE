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

    private float bonusMaxHealth =0;

    public bool IsDead => isDead;
    public int GetTotalMaxHealth() => data.maxHealth + Mathf.RoundToInt(bonusMaxHealth);

    void Start()
    {
        player = GetComponent<PlayerManager>();
        currentHealth = GetTotalMaxHealth();
        PlayerEvents.OnHealthChanged?.Invoke(currentHealth, GetTotalMaxHealth());
    }

    public void UpdateMaxHealth(float value, StatModifier.StatOperation op)
    {
        int oldMax = GetTotalMaxHealth();
        
        if (op == StatModifier.StatOperation.Add) 
        {
            bonusMaxHealth += value;
        }
        else if (op == StatModifier.StatOperation.Multiply) 
        {
            bonusMaxHealth = (oldMax * value) - data.maxHealth;
        }

        int healthIncrease = GetTotalMaxHealth() - oldMax;
        if (healthIncrease > 0)
        {
            currentHealth += healthIncrease;
        }
        
        PlayerEvents.OnHealthChanged?.Invoke(currentHealth, GetTotalMaxHealth());
    }

    public void TakeDamage(int damage)
    {
        if (isDead || isInvincible) return;

        currentHealth -= damage;
        PlayerEvents.OnHealthChanged?.Invoke(currentHealth, GetTotalMaxHealth());
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
        yield return new WaitForSeconds(data.invincibilityDuration);
        isInvincible = false;
    }

    private IEnumerator HitFlashRoutine()
    {
        SpriteRenderer sr = player.anim.GetComponentInChildren<SpriteRenderer>();
        Transform t = sr.transform;

        Color originalColor = sr.color;
        Vector3 originalScale = t.localScale;

        // Flash trắng
        sr.color = new Color(2f,2f,2f,1f);
        t.localScale = originalScale * 1.15f;

        yield return new WaitForSeconds(0.08f);

        // Đỏ nhẹ
        sr.color = new Color(2.4f, 0.6f, 0.6f, 1f);
        t.localScale = originalScale;

        yield return new WaitForSeconds(0.075f);

        sr.color = originalColor;
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
        GameManager.Instance.EndGame();
    }
}