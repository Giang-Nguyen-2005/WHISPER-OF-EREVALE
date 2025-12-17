using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public float timeAttack = 0.5f;
    public float timeNextAttack = 0f;
    [SerializeField] private Vector2 sizeSpearHitbox = new Vector2(0.5f, 0.25f);
    public float attackOffset = 0.4f;
    [SerializeField] private Vector2 debugHitbox = new Vector2(0.1f, 0f);
    public LayerMask targetSpear;
    public int weaponType;

    private PlayerManager player;

    void Start()
    {
        player = GetComponent<PlayerManager>();
    }

    void Update()
    {
        if (player.inputHandler.isWeapon1KeyDown) weaponType = 0;
        if (player.inputHandler.isWeapon2KeyDown) weaponType = 1;

        if (player.inputHandler.isAttackKeyDown && weaponType == 1 && Time.time > timeNextAttack)
        {
            timeNextAttack = Time.time + timeAttack;
            player.anim.TriggerAttack();
            SpearAttack();
        }
    }

    private void SpearAttack()
    {
        Vector2 positionHitBox = PositionHitBox();
        
        // Chuyển thành độ
        float angle = Mathf.Atan2(player.movement.lastDirection.y, player.movement.lastDirection.x) * Mathf.Rad2Deg;

        Collider2D[] hitboxSpear = Physics2D.OverlapBoxAll(positionHitBox, sizeSpearHitbox, angle, targetSpear);

        if (hitboxSpear.Length > 0)
        {
            Debug.Log("-----------------");
            Debug.Log("Trúng " + hitboxSpear.Length + " vật thể!");
            foreach (Collider2D target in hitboxSpear)
            {
                if (target.gameObject == gameObject) continue;
                Debug.Log("Đâm trúng: " + target.name);
            }
        }
    }

    private Vector2 PositionHitBox()
    {
        Vector2 lastDirection = player.movement.lastDirection;
        Vector2 possionAttack = (Vector2)transform.position + lastDirection * attackOffset;
        
        if (lastDirection == Vector2.down) possionAttack -= debugHitbox;
        else if (lastDirection == Vector2.up) possionAttack += debugHitbox;
        
        return possionAttack;
    }

    void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;
        if (player == null) return;

        Vector2 lastDirection = player.movement.lastDirection;
        if (lastDirection == Vector2.zero) return;

        Gizmos.color = Color.yellow;
        Vector2 positonHitBox = PositionHitBox();
        
        Matrix4x4 oldMatrix = Gizmos.matrix;
        float angle = Mathf.Atan2(lastDirection.y, lastDirection.x) * Mathf.Rad2Deg;
        Gizmos.matrix = Matrix4x4.TRS(positonHitBox, Quaternion.Euler(0, 0, angle), Vector3.one);
        Gizmos.DrawWireCube(Vector3.zero, sizeSpearHitbox);
        Gizmos.matrix = oldMatrix;
    }
}