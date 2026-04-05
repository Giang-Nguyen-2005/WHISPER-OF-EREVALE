using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public PlayerData data;
    
    public float currentSpeed;
    public Vector2 lastDirection; // Hướng nhân vật nhìn (theo chuột)
    public bool isDashing = false;

    public float bonusSpeed=0f;

    private PlayerManager player;

    void Start()
    {
        player = GetComponent<PlayerManager>();
        lastDirection = Vector2.down;
    }

    void Update()
    {
        // Luôn cập nhật hướng nhìn dựa trên vị trí chuột
        Vector2 lookDir = (player.inputHandler.mouseWorldPosition - (Vector2)transform.position).normalized;
        if (lookDir != Vector2.zero)
        {
            lastDirection = lookDir;
        }

        if (player.inputHandler.isDashKeyDown && !isDashing && player.inputHandler.moveInput != Vector2.zero)
        {
            StartCoroutine(Dashing());
        }
    }

    void FixedUpdate()
    {
        if (!isDashing)
        {
            currentSpeed = (player.inputHandler.isRunning ? data.runSpeed : data.walkSpeed)+ bonusSpeed;
            player.rb.linearVelocity = player.inputHandler.moveInput * currentSpeed;
        }
    }

    private IEnumerator Dashing()
    {
        isDashing = true;
        // Dash theo hướng phím bấm
        player.rb.linearVelocity = player.inputHandler.moveInput * data.dashSpeed;
        player.anim.TriggerDash();
        yield return new WaitForSeconds(data.dashDuration);
        isDashing = false;
    }
}