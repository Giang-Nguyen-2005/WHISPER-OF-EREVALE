using System;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public abstract class EnemyBase : MonoBehaviour, IDamageable, IPoolable
{
    public EnemyData data;
    protected int currentHealth;
    protected bool isDead = false;
    protected bool isAttacking = false;
    [SerializeField] protected Transform playerTransform;
    protected Rigidbody2D rb;
    protected Animator anim;
    public bool IsDead => isDead;
    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }
    public void OnSpawn()
    {
        ResetEnemy();
    }
    public void SetTarget(Transform target)
    {
        playerTransform = target;
    }
    public virtual void ResetEnemy()
    {
        isDead = false;
        isAttacking = false;
        currentHealth = data.maxHealth;
        GetComponent<Collider2D>().enabled = true;
        if (rb != null) rb.linearVelocity = Vector2.zero;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        currentHealth = data.maxHealth;
    }
    protected virtual void FixedUpdate()
    {
        if (isDead || playerTransform == null || isAttacking)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        MoveTowardsPlayer();
    }
    // Update is called once per frame
    protected virtual void Update()
    {
        if (isDead || playerTransform == null) return;
        float dist = Vector2.Distance(transform.position, playerTransform.position);
        if (dist <= data.attackRange && !isAttacking)
        {
            AttackLogic();
        }
    }
    protected void MoveTowardsPlayer()
    {
        Vector2 direction = (playerTransform.position - transform.position).normalized;
        rb.linearVelocity = direction * data.baseSpeed;
        anim.SetFloat("InputX", direction.x);
        anim.SetFloat("InputY", direction.y);

    }
    protected abstract void AttackLogic();

    public virtual void TakeDamage(int dmg)
    {
        if (isDead) return;
        currentHealth -= dmg;
        if (currentHealth <= 0) Die();
        else anim.SetTrigger("Hurt");
    }
    protected virtual void Die()
    {
        isDead = true;
        rb.linearVelocity = Vector2.zero;
        anim.SetTrigger("Death");
        GetComponent<Collider2D>().enabled = false;
        Invoke("Deactivate", 0.25f);
        //drop gem
        ObjectPooler.Instance.GetFromPool("ExpGem", transform.position, Quaternion.identity);
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        // Nếu chạm vào Player
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.TryGetComponent(out IDamageable playerHealth))
            {
                // Cắn theo sát thương trong data
                playerHealth.TakeDamage(data.contactDamage);
            }
        }
    }

    //For AnimationEvent Attack
    public virtual void PerformAttackLogic()
    {
        if (isDead || playerTransform == null) return;

        float dist = Vector2.Distance(transform.position, playerTransform.position);

        if (dist <= data.attackRange)
        {
            if (playerTransform.TryGetComponent(out IDamageable playerHealth))
            {
                playerHealth.TakeDamage(data.contactDamage);
                ////
            }
        }
    }
    private void Deactivate() => gameObject.SetActive(false);

}
