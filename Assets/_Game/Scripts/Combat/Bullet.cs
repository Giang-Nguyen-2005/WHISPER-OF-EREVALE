using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 12.0f;
    public int damage = 15;
    public float lifeTime = 2.0f;

    void OnEnable()
    {
      Invoke("Deactivate",lifeTime);// gọi hàm deactivate sau lifeTime  
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.TryGetComponent(out IDamageable hitTarget))// nếu trúng đứa có IDamageable thì true
        {
            hitTarget.TakeDamage(damage);
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
        transform.Translate(Vector2.right * speed * Time.deltaTime);//forward là trục x( dùng mathf.atan2 để xoay)
    }
}
