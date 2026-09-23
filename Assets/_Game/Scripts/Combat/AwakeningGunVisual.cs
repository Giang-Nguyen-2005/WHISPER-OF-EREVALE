using UnityEngine;

public class AwakeningGunVisual : MonoBehaviour 
{
    private PlayerManager player; 

    void Start()
    {
        player = GetComponentInParent<PlayerManager>();
    }

    void Update()
    {
        if (player == null) return;

        // Xoay súng theo hướng di chuyển cuối cùng của Player
        Vector2 dir = player.movement.lastDirection;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        // Trick lật súng: Nếu quay sang trái (x < 0) thì lật cái hình súng lại cho khỏi bị úp ngược
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            sr.flipY = (dir.x < 0);
        }
    }
}