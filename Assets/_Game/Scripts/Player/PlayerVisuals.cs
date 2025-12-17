using UnityEngine;

public class PlayerVisuals : MonoBehaviour
{
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private Animator shadowAnimator;
    public float direction;

    private PlayerManager player;

    void Start()
    {
        player = GetComponent<PlayerManager>();
        
        // Logic tìm Animator cũ
        if (playerAnimator == null) 
            playerAnimator = GameObject.Find("Visuals").GetComponent<Animator>();
        if (shadowAnimator == null) 
            shadowAnimator = GameObject.Find("Shadow").GetComponent<Animator>();
    }

    void Update()
    {
        if (playerAnimator == null) return;

        Vector2 moveInput = player.inputHandler.moveInput;
        Vector2 lastDirection = player.movement.lastDirection;
        float currentSpeed = player.movement.currentSpeed;
        bool isRunning = player.inputHandler.isRunning;

        playerAnimator.SetFloat("Speed", moveInput.magnitude * currentSpeed, 0.03f, Time.deltaTime);

        if (player.inputHandler.isJumpKeyDown)
        {
            playerAnimator.SetTrigger("Jump");
        }

        if (moveInput.magnitude > 0)//magnitude la do dai cua 1 vecto c^2 = a^2 +b^2
        {
            direction = isRunning ? 1 : 0.5f;
            playerAnimator.SetFloat("InputX", lastDirection.x * direction);
            playerAnimator.SetFloat("InputY", lastDirection.y * direction);
            shadowAnimator.SetFloat("InputX", moveInput.x);
            shadowAnimator.SetFloat("InputY", moveInput.y);
        }
        else
        {
            playerAnimator.SetFloat("InputX", lastDirection.x);
            playerAnimator.SetFloat("InputY", lastDirection.y);
        }

        playerAnimator.SetInteger("WeaponType", player.combat.weaponType);
    }

    //Các hàm kích hoạt Trigger
    public void TriggerDash() 
    {
        playerAnimator.SetTrigger("Dash");
        shadowAnimator.SetTrigger("Dash Shadow");
    }
    public void TriggerAttack() => playerAnimator.SetTrigger("Attack");
    public void TriggerDeath() 
    {
        playerAnimator.SetTrigger("Death");
        shadowAnimator.SetTrigger("Death Shadow");
    }
}