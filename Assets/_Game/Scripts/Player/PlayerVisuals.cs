using UnityEngine;

public class PlayerVisuals : MonoBehaviour
{
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private Animator shadowAnimator;

    private PlayerManager player;

    void Start()
    {
        player = GetComponent<PlayerManager>();
        if (playerAnimator == null) playerAnimator = transform.Find("Visuals")?.GetComponent<Animator>();
        if (shadowAnimator == null) shadowAnimator = transform.Find("Shadow")?.GetComponent<Animator>();
    }

    void Update()
    {
        if (playerAnimator == null) return;

        Vector2 moveInput = player.inputHandler.moveInput;
        Vector2 lookDir = player.movement.lastDirection; // Hướng nhìn theo chuột
        float speed = moveInput.magnitude * player.movement.currentSpeed;

        // 1. Tốc độ di chuyển để chuyển Idle -> Walk/Run
        playerAnimator.SetFloat("Speed", speed, 0.03f, Time.deltaTime);

        float direction =player.inputHandler.isRunning? 1 : 0.5f;

        // 2. Ép hướng Animator theo vị trí chuột 
        playerAnimator.SetFloat("InputX", lookDir.x*direction);
        playerAnimator.SetFloat("InputY", lookDir.y*direction);

        // 3. Shadow
        if (moveInput.magnitude > 0)
        {
            shadowAnimator.SetFloat("InputX", moveInput.x);
            shadowAnimator.SetFloat("InputY", moveInput.y);
        }

        if (player.inputHandler.isJumpKeyDown) playerAnimator.SetTrigger("Jump");
    }

    public void TriggerDash() { playerAnimator.SetTrigger("Dash"); shadowAnimator.SetTrigger("Dash Shadow"); }
    public void TriggerAttack() => playerAnimator.SetTrigger("Attack");
    public void TriggerDeath() { playerAnimator.SetTrigger("Death"); shadowAnimator.SetTrigger("Death Shadow"); }

    public void TriggerShoot() { playerAnimator.SetTrigger("TriggerShoot"); }
    public void SetShootBool(bool isShooting)
    {
        playerAnimator.SetBool("Shoot", isShooting);
    }
    public void SetReloadBool(bool isReloading){playerAnimator.SetBool("isReloading",isReloading);}
    public void UpdateWeaponAnimation(int id)
{
    playerAnimator.SetInteger("WeaponType", id);
}
}