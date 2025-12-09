using UnityEngine;
using System.Collections;
public class PlayerController : MonoBehaviour
{   
    public float runSpeed = 2.2f;
    public float walkSpeed=1.2f;

    public float dashSpeed=5.2f;

    public float timeDash=0.18f;
    [SerializeField] private float currentSpeed;

    [SerializeField] Vector2 lastDirection;
    public float direction;
    public bool isRunning=false;

    public bool isDashing=false;

    public bool isJumping=false; 
    private Rigidbody2D rb;

    private Vector2 moveInput;
    private Animator animator;
    public GameObject shadow;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();

        animator = GetComponentInChildren<Animator>();
        shadow=GameObject.Find("Visuals/Shadow");
    }
    
    void HandleInput()
    {
        float moveY =Input.GetAxisRaw("Vertical");
        float moveX=Input.GetAxisRaw("Horizontal");

        moveInput= new Vector2(moveX,moveY).normalized;
        //Move
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isRunning=true;
        }
        else isRunning=false;
        //Dash
        if (Input.GetKeyDown(KeyCode.Z)&&!isDashing)
        {
            StartCoroutine(Dashing());
        }

        currentSpeed = isRunning ? runSpeed : walkSpeed;
    }
    
    // Update is called once per frame
    void Update()
    {
        HandleInput();
        UpdateAnimation();

    }
    void UpdateAnimation()
    {
        if (animator==null) return;
        else
        {
            //lam muot
            animator.SetFloat("Speed", moveInput.magnitude*currentSpeed,0.03f,Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("Jump");
        }
            //Cập nhật hướng
            if (moveInput.magnitude > 0)//magnitude la do dai cua 1 vecto c^2 = a^2 +b^2
        {
            direction = isRunning ? 1 : 0.5f;// 1 là chạy , 0.5 là walk
                animator.SetFloat("InputX", moveInput.x*direction);
                animator.SetFloat("InputY", moveInput.y*direction);
        }

        }
            
        
    }
    void FixedUpdate()
    {   
        if (!isDashing)
        {
            rb.linearVelocity = moveInput * currentSpeed;
        }
    }
    private IEnumerator Dashing()
        {
            isDashing=true;
            
            rb.linearVelocity= moveInput*dashSpeed;
            animator.SetTrigger("Dash_Shadow");
            animator.SetTrigger("Dash");
            yield return new WaitForSeconds(timeDash);
            isDashing=false;
            
        }
}
