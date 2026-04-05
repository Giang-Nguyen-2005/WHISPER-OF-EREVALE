using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Bullet : MonoBehaviour, IPoolable
{
    public PlayerManager player;
    public GunData data;
    public float lifeTime = 2.0f;
    [SerializeField] private LayerMask playerLayer;

    void Awake()
    {
        if (player == null)
        {
            player=GameObject.FindWithTag("Player").GetComponent<PlayerManager>();
        }
    }
    public void OnSpawn()
    {
    
        CancelInvoke();
        Invoke("Deactivate", lifeTime);// gọi hàm deactivate sau lifeTime  
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        // Cần review lại hàm if này
        if (((1 << other.gameObject.layer) & playerLayer) != 0) return;
        if (other.TryGetComponent(out IDamageable hitTarget))// nếu trúng đứa có IDamageable thì true
        {
            hitTarget.TakeDamage(data.damage+ player.combat.bonusDamage);
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
        transform.Translate(Vector2.right * data.speed * Time.deltaTime);//forward là trục x( dùng mathf.atan2 để xoay)
    }
}
