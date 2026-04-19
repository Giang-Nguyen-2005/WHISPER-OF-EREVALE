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
        Vector2 lookDir = player.movement.lastDirection; 
        float speed = moveInput.magnitude * player.movement.currentSpeed;

        playerAnimator.SetFloat("Speed", speed, 0.03f, Time.deltaTime);

        float blendMultiplier = player.movement.isRunSkillActive ? 1f : 0.5f;

        playerAnimator.SetFloat("InputX", lookDir.x * blendMultiplier);
        playerAnimator.SetFloat("InputY", lookDir.y * blendMultiplier);

        if (moveInput.magnitude > 0.05f)
        {
            shadowAnimator.SetFloat("InputX", moveInput.x);
            shadowAnimator.SetFloat("InputY", moveInput.y);
        }
    }

    public void TriggerDash() 
    { 
        playerAnimator.SetTrigger("Dash"); 
        if(shadowAnimator != null) shadowAnimator.SetTrigger("Dash Shadow"); 
    }

    public void TriggerAttack() => playerAnimator.SetTrigger("Attack");
    public void TriggerDeath() { playerAnimator.SetTrigger("Death"); if(shadowAnimator != null) shadowAnimator.SetTrigger("Death Shadow"); }
    public void TriggerShoot() => playerAnimator.SetTrigger("TriggerShoot");
    public void SetShootBool(bool isShooting) => playerAnimator.SetBool("Shoot", isShooting);
    public void SetReloadBool(bool isReloading) => playerAnimator.SetBool("isReloading", isReloading);
    public void UpdateWeaponAnimation(int id) => playerAnimator.SetInteger("WeaponType", id);
}