using System.Collections;
using UnityEngine;

public class MeleeEnemy : EnemyBase
{
    protected override void AttackLogic()
    {
        rb.linearVelocity = Vector2.zero;
        StartCoroutine(AttackRoutine());
    }

    IEnumerator AttackRoutine()
    {
        isAttacking=true;
        rb.linearVelocity=Vector2.zero;
        anim.SetTrigger("Attack");
        yield return new WaitForSeconds(data.attackCooldown);
        isAttacking=false;
    }
}

