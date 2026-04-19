using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Stats")]
    public PlayerData data;
    public float currentSpeed;
    public Vector2 lastDirection;
    
    [Header("States")]
    public bool isDashing = false;
    public bool isRunSkillActive = false;

    [Header("Run Skill (Z)")]
    [SerializeField] private float runDuration = 10f;
    [SerializeField] private float runCooldown = 15f;
    private float nextRunTime;

    [Header("Dash Skill (Space)")]
    [SerializeField] private float dashCooldown = 7f;
    private float nextDashTime;

    public float bonusSpeed = 0f;
    private PlayerManager player;

    void Start()
    {
        player = GetComponent<PlayerManager>();
        lastDirection = Vector2.down;
    }

    void Update()
    {
        UpdateLookDirection();
        HandleSkillsInput();
    }

    private void UpdateLookDirection()
    {
        Vector2 lookDir = (player.inputHandler.mouseWorldPosition - (Vector2)transform.position).normalized;
        if (lookDir != Vector2.zero) lastDirection = lookDir;
    }

    private void HandleSkillsInput()
    {
        if (player.inputHandler.isRunSkillDown && Time.time >= nextRunTime && !isDashing)
        {
            StartCoroutine(RunSkillRoutine());
        }

        if (player.inputHandler.isDashKeyDown && Time.time >= nextDashTime && !isDashing && player.inputHandler.moveInput != Vector2.zero)
        {
            StartCoroutine(Dashing());
        }
    }

    void FixedUpdate()
    {
        if (isDashing) return;

        float baseMoveSpeed = isRunSkillActive ? data.runSpeed : data.walkSpeed;
        currentSpeed = baseMoveSpeed + bonusSpeed;
        
        player.rb.linearVelocity = player.inputHandler.moveInput * currentSpeed;
    }

    private IEnumerator RunSkillRoutine()
    {
        isRunSkillActive = true;
        nextRunTime = Time.time + runCooldown;

        yield return new WaitForSeconds(runDuration);

        isRunSkillActive = false;
    }

    private IEnumerator Dashing()
    {
        isDashing = true;
        nextDashTime = Time.time + dashCooldown;

        player.rb.linearVelocity = player.inputHandler.moveInput * data.dashSpeed;
        player.anim.TriggerDash();

        yield return new WaitForSeconds(data.dashDuration);
        isDashing = false;
    }
}