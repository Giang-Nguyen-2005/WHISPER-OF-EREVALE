using System.Collections;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class RangedEnemy : EnemyBase
{
    private RangedEnemyData rangedData;
    protected override void Awake()
    {
        base.Awake();
        rangedData = (RangedEnemyData)data;
    }

    protected override void FixedUpdate()
    {
        if (isDead || playerTransform == null) return;

        Vector2 direction = (playerTransform.position - transform.position).normalized;
        anim.SetFloat("InputX", direction.x);
        anim.SetFloat("InputY", direction.y);

        if (isAttacking)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer > rangedData.stopDistance)
        {
            MoveTowardsPlayer();
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }
    protected override void AttackLogic()
    {
        if (!isAttacking)
        {
            StartCoroutine(ShootRoutine());
        }
    }
    IEnumerator ShootRoutine()
    {
        isAttacking = true;
        rb.linearVelocity = Vector2.zero;

        anim.SetTrigger("Attack");
        
        yield return new WaitForSeconds(data.attackCooldown);

        isAttacking = false;
    }
    void SpawnBullet()
    {
        Vector2 direction = (playerTransform.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        Vector2 spawnPos = (Vector2)transform.position + direction * 0.5f;

        GameObject bulletObj = ObjectPooler.Instance.GetFromPool(rangedData.bulletTag,
        spawnPos,
        Quaternion.Euler(0, 0, angle));

        if (bulletObj.TryGetComponent(out Bullet bulletScpript))
        {
            LayerMask enemyLayer = LayerMask.GetMask("TargetSpear");
            bulletScpript.Init(data.contactDamage, rangedData.bulletSpeed, enemyLayer, rangedData.bulletLifeTime , data.shakeData);
        }
    }
    public override void PerformAttackLogic()
    {
        if (isDead || playerTransform == null) return;
        SpawnBullet();
    }

}
