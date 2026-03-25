using UnityEngine;

public class SpearWeapon : WeaponBase
{
    [Header("Hitbox Settings")]
    [SerializeField] private Vector2 sizeSpearHitbox = new Vector2(0.5f, 0.25f);
    [SerializeField] private float attackOffset = 0.4f;
    [SerializeField] private Vector2 debugHitbox = new Vector2(0.1f, 0f);
    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private int damage = 30;
    [SerializeField] private float attackCooldown = 0.5f;

    private PlayerManager player;
    private float nextAttackTime;

    public override void Setup(PlayerManager _player) => player = _player;

    public override void Attack()
    {
        if (Time.time < nextAttackTime) return;
        nextAttackTime = Time.time + attackCooldown;

        // BƯỚC 1: Chỉ kích hoạt Animation đâm giáo
        player.anim.TriggerAttack();
    }

    //Animation Event
    public override void OnAnimationAttackEvent()
    {
        Vector2 hitBoxPos = CalculateHitBoxPosition();
        float angle = Mathf.Atan2(player.movement.lastDirection.y, player.movement.lastDirection.x) * Mathf.Rad2Deg;

        Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(hitBoxPos, sizeSpearHitbox, angle, targetLayer);

        foreach (Collider2D target in hitEnemies)
        {
            if (target.gameObject == player.gameObject) continue;
            if (target.TryGetComponent(out IDamageable hitTarget))
            {
                hitTarget.TakeDamage(damage);
            }
        }
    }

    private Vector2 CalculateHitBoxPosition()
    {
        Vector2 lastDirection = player.movement.lastDirection;
        Vector2 positionAttack = (Vector2)player.transform.position + lastDirection * attackOffset;

        if (lastDirection == Vector2.down) positionAttack -= debugHitbox;
        else if (lastDirection == Vector2.up) positionAttack += debugHitbox;

        return positionAttack;
    }

    private void OnDrawGizmosSelected()
    {
        if (player == null) return;
        Gizmos.color = Color.yellow;
        Vector2 positionHitBox = CalculateHitBoxPosition();
        Matrix4x4 oldMatrix = Gizmos.matrix;
        float angle = Mathf.Atan2(player.movement.lastDirection.y, player.movement.lastDirection.x) * Mathf.Rad2Deg;
        Gizmos.matrix = Matrix4x4.TRS(positionHitBox, Quaternion.Euler(0, 0, angle), Vector3.one);
        Gizmos.DrawWireCube(Vector3.zero, sizeSpearHitbox);
        Gizmos.matrix = oldMatrix;
    }
}