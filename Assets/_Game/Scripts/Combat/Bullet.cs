using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Bullet : MonoBehaviour, IPoolable
{
    public GunData data;
    private float totalDamage;
    private float bulletSpeed;
    public float lifeTime = 2.0f;
    private LayerMask targetLayer;

    public void Init(float damage, float speed,LayerMask target)
    {
        this.bulletSpeed =speed;
        this.totalDamage =damage;
        this.targetLayer=target;
    }
    public void OnSpawn()
    {
    
        CancelInvoke();
        Invoke("Deactivate", lifeTime);// gọi hàm deactivate sau lifeTime  
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
        transform.position += transform.right * data.speed * Time.deltaTime;
    }
}
