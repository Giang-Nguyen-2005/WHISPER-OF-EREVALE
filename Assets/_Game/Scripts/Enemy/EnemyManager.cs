using System.Collections;
using UnityEngine;

public class EnemyManager : MonoBehaviour, IDamageable
{
    public bool IsDead => isDead;
    [Header("Settings")]
    public float runSpeed = 1.7f;
    public float walkSpeed=0.8f;
    public float distanceDetect = 3.0f;
    public float attackRange = 1.0f;
    public float attackCooldown = 1.5f;
    public int maxHealth = 100;

    public Vector3 defaultPosition;

    public Transform player;
    public Animator anim;
    public Rigidbody2D rb;

    public float distance;
    public int currentHealth;
    public bool isAttacking = false;
    private bool isDead = false;

    public Vector3 debugPosition;

    void Start()
    {
        defaultPosition= transform.position;
        currentHealth = maxHealth;
        if (player == null) player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        float distanceToDefault = Vector2.Distance(transform.position, defaultPosition);
        debugPosition=transform.position;
        if (isDead) return;

        if (player == null) return;
        distance = Vector2.Distance(transform.position, player.position);
        if (isAttacking) 
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }
        if (distance <= attackRange)
        {
            rb.linearVelocity = Vector2.zero;
            anim.SetBool("Is Move", false);
            StartCoroutine(AttackRoutine());
        }
        
        else if (distance <= distanceDetect)
        {
            MoveTowardsPlayer();
        }
        else if (distanceToDefault>0.1f)
        {
            anim.SetBool("Is Move",true);
            Vector2 defaultInput= (defaultPosition-transform.position).normalized;

            rb.linearVelocity= new Vector2(defaultInput.x,defaultInput.y)*walkSpeed;
            anim.SetFloat("InputX",defaultInput.x*0.5f);
            anim.SetFloat("InputY",defaultInput.y*0.5f);// để chuyển sang walk trong blend tree 
        }
        else if(distanceToDefault<=0.1f)
        {
            transform.position = defaultPosition;
            anim.SetBool("Is Move", false);
            rb.linearVelocity = Vector2.zero;
        }
    }

    void MoveTowardsPlayer()
    {
        Vector2 input = (player.position - transform.position).normalized;
        
        anim.SetBool("Is Move", true);
        anim.SetFloat("InputX", input.x);
        anim.SetFloat("InputY", input.y);
        
        rb.linearVelocity = new Vector2(input.x, input.y) * runSpeed;
    }

    // COROUTINE: Quy trình tấn công
    IEnumerator AttackRoutine()
    {
        isAttacking = true; 
        anim.SetTrigger("Attack");
        yield return new WaitForSeconds(attackCooldown);
        isAttacking = false;
    }

    public void TakeDamage(int dmg)
    {
        if (isDead) return;
        currentHealth -= dmg;

        if (currentHealth <= 0) Die();
        else anim.SetTrigger("Hurt");
    }

    private void Die()
    {
        isDead = true;
        anim.SetTrigger("Death");
        rb.linearVelocity = Vector2.zero;
        GetComponent<Collider2D>().enabled = false;
        StopAllCoroutines(); 
        
        Debug.Log("Slime cút");
        Destroy(gameObject, 0.5f);
    }
}