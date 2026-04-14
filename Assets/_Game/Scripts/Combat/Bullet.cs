using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Bullet : MonoBehaviour, IPoolable
{
    
    private float totalDamage;
    private float bulletSpeed;
    private float lifeTime;
    private LayerMask ignoreLayer;
    private CameraShakeData shakeData;

    public void Init(float damage, float speed,LayerMask target, float life,CameraShakeData shake = null)
    {   
        this.bulletSpeed =speed;
        this.totalDamage =damage;
        this.ignoreLayer=target;
        this.lifeTime =life;
        this.shakeData = shake;
        CancelInvoke();
        Invoke("Deactivate", lifeTime);
    }
    public void OnSpawn()
    {

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        // Cần review lại hàm if này
        if (((1 << other.gameObject.layer) & ignoreLayer) != 0) return;
        if (other.TryGetComponent(out IDamageable hitTarget))// nếu trúng đứa có IDamageable thì true
        {
            if (other.CompareTag("Player") && shakeData != null)
            {
                PlayerEvents.OnPlayerHit?.Invoke(shakeData);
            }
            hitTarget.TakeDamage(Mathf.RoundToInt(totalDamage));
            Deactivate();
        }
        
    }
    void Deactivate()
    {
        CancelInvoke();// // hủy lệnh đếm ngược Invoke ở trên (để tránh thu hồi nhầm lần nữa)
        gameObject.SetActive(false);//đồng thời tắt active của viên đạn
    }


    // Update is called once per frame
    void Update()
    {
        transform.position += transform.right * bulletSpeed * Time.deltaTime;
    }
}
