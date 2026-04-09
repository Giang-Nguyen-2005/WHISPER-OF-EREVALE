using System.Collections;
using UnityEngine;
public class GunWeapon : WeaponBase
{
    [SerializeField] private GunData data;
    private PlayerManager player;
    private float nextAttackTime;

    [SerializeField] private string tagBullet;
    private int currentAmmo;
    private bool isReloading = false;

    public override void Setup(PlayerManager _player)
    {
        player = _player;
        currentAmmo=data.magSize;
    }

    public override void Attack()
    {
        if(isReloading) return;

        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }
        player.anim.SetShootBool(true);

        if (Time.time < nextAttackTime) return;
        nextAttackTime = Time.time + (data.fireRate + player.combat.bonusFireRate);


        Shoot();
    }

    public override void StopAttack()
    {
        player.anim.SetShootBool(false);
    }

    private void Shoot()
    {
        currentAmmo--;
        // pos của đạn
        Vector2 spawnPos = (Vector2)player.transform.position + player.movement.lastDirection * 0.4f;

        // góc xoay dựa trên last direction của player
        float angle = Mathf.Atan2(player.movement.lastDirection.y, player.movement.lastDirection.x) * Mathf.Rad2Deg;
        Quaternion spawnRot = Quaternion.Euler(0, 0, angle);

        GameObject bulletObj=ObjectPooler.Instance.GetFromPool(tagBullet, spawnPos, spawnRot);

        if(bulletObj.TryGetComponent(out Bullet bullet))
        {
            LayerMask playerLayer = LayerMask.GetMask("Player");
            bullet.Init(player.combat.bonusDamage + data.damage, data.speed,playerLayer,data.bulletLifeTime);
        }

        player.anim.TriggerShoot();

        AudioManager.Instance.PlaySFX(AudioManager.Instance.shootSFX);
    }

    IEnumerator Reload()
    {
        isReloading=true;
        player.GetComponentInChildren<Animator>().ResetTrigger("TriggerShoot");
        player.anim.SetReloadBool(true);
        AudioManager.Instance.PlaySFX(AudioManager.Instance.reloadSFX);
        yield return new WaitForSeconds(data.reloadTime);
        currentAmmo =data.magSize;
        isReloading=false;
        player.anim.SetReloadBool(false);
    }
}