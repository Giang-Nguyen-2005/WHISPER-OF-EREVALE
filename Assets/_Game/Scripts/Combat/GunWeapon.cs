using UnityEngine;
public class GunWeapon : WeaponBase
{
    [SerializeField] private GunData data;
    private PlayerManager player;
    private float nextAttackTime;

    public override void Setup(PlayerManager _player) => player = _player;

    public override void Attack()
    {
        player.anim.SetShootBool(true);

        if (Time.time < nextAttackTime) return;
        nextAttackTime = Time.time + data.fireRate; 

        
        Shoot();
    }

    public override void StopAttack()
    {
        player.anim.SetShootBool(false);
    }

    private void Shoot()
    {
        // pos của đạn
        Vector2 spawnPos = (Vector2)player.transform.position + player.movement.lastDirection * 0.4f;

        // góc xoay dựa trên last direction của player
        float angle = Mathf.Atan2(player.movement.lastDirection.y, player.movement.lastDirection.x) * Mathf.Rad2Deg;
        Quaternion spawnRot = Quaternion.Euler(0, 0, angle);

        ObjectPooler.Instance.GetFromPool("Bullet", spawnPos, spawnRot);
        
        player.anim.TriggerShoot();
    }
}