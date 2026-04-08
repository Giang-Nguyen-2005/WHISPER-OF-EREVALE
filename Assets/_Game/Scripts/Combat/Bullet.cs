using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Bullet : MonoBehaviour, IPoolable
{
    
    private float totalDamage;
    private float bulletSpeed;
    private float lifeTime;
    private LayerMask targetLayer;

    public void Init(float damage, float speed,LayerMask target, float life)
    {   
        this.bulletSpeed =speed;
        this.totalDamage =damage;
        this.targetLayer=target;
        this.lifeTime =life;
        CancelInvoke();
        Invoke("Deactivate", lifeTime);
    }
    public void OnSpawn()
    {

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        // Cần review lại hàm if này
        if (((1 << other.gameObject.layer) & targetLayer) != 0) return;
        if (other.TryGetComponent(out IDamageable hitTarget))// nếu trúng đứa có IDamageable thì true
        {
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
