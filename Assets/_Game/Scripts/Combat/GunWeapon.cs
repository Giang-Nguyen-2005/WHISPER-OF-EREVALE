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
        GameObject bullet = ObjectPooler.Instance.GetPooledObject();
        if (bullet != null)
        {
            bullet.transform.position = (Vector2)player.transform.position + player.movement.lastDirection * 0.4f;
            float angle = Mathf.Atan2(player.movement.lastDirection.y, player.movement.lastDirection.x) * Mathf.Rad2Deg;
            bullet.transform.rotation = Quaternion.Euler(0, 0, angle);
            bullet.SetActive(true);
        }
        player.anim.TriggerShoot();
    }
}