using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float runSpeed = 2.2f;
    public float walkSpeed = 1.2f;
    public float dashSpeed = 5.2f;
    public float timeDash = 0.18f;
    
    public float currentSpeed;
    public Vector2 lastDirection;
    
    public bool isDashing = false;

    private PlayerManager player;

    void Start()
    {
        player = GetComponent<PlayerManager>();
        lastDirection = Vector2.down;
    }

    void Update()
    {
        if (player.inputHandler.moveInput != Vector2.zero)
        {
            lastDirection = player.inputHandler.moveInput;
        }

        if (player.inputHandler.isDashKeyDown && !isDashing)
        {
            StartCoroutine(Dashing());
        }
    }

    void FixedUpdate()
    {
        if (!isDashing)
        {
            currentSpeed = player.inputHandler.isRunning ? runSpeed : walkSpeed;
            player.rb.linearVelocity = player.inputHandler.moveInput * currentSpeed;
        }
    }

    private IEnumerator Dashing()
    {
        isDashing = true;
        player.rb.linearVelocity = player.inputHandler.moveInput * dashSpeed;
        
        player.anim.TriggerDash();
        
        yield return new WaitForSeconds(timeDash);
        isDashing = false;
    }
}