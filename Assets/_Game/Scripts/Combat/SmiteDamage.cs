using UnityEngine;

public class SmiteDamage : MonoBehaviour
{
    private int damage;
    private PlayerManager player;

    public void Init(int smiteDamage, PlayerManager pManager)
    {
        damage = smiteDamage;
        player = pManager;
        
        Destroy(gameObject, 0.23f); 
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out IDamageable hitTarget))
        {
            if (other.CompareTag("Player")) return;
            
            int finalDamage = damage;
            if (player != null && player.combat != null)
            {
                finalDamage = Mathf.RoundToInt(damage + player.combat.bonusDamage);
            }

            hitTarget.TakeDamage(finalDamage);
            
            GetComponent<Collider2D>().enabled = false;
        }
    }
}