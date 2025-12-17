using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public InputHandler inputHandler;
    public PlayerMovement movement;
    public PlayerCombat combat;
    public PlayerVisuals anim;
    public Rigidbody2D rb;

    void Awake()
    {
        inputHandler = GetComponent<InputHandler>();
        movement = GetComponent<PlayerMovement>();
        combat = GetComponent<PlayerCombat>();
        anim = GetComponent<PlayerVisuals>();
        rb = GetComponent<Rigidbody2D>();
    }

    //death
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("death"))
        {
            anim.TriggerDeath();
        }
    }
}