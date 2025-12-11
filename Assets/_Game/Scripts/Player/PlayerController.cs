using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
public class PlayerController : MonoBehaviour
{   
    public float runSpeed = 2.2f;
    public float walkSpeed=1.2f;

    public float dashSpeed=5.2f;
    public float timeDash=0.18f;
    [SerializeField] private float currentSpeed;
    [SerializeField] Vector2 lastDirection;
    public float direction;

    public int weaponType;
    public bool isRunning=false;

    public bool isDashing=false;

    public bool isJumping=false; 
    private Rigidbody2D rb;

    private Vector2 moveInput;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private Animator shadowAnimator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();

        playerAnimator = GameObject.Find("Visuals").GetComponent<Animator>();
        shadowAnimator=GameObject.Find("Shadow").GetComponent<Animator>();
        
        //playerAnimator = GetComponentInChildren<Animator>();
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
        if(Input.GetKeyDown(KeyCode.Alpha2)|| Input.GetKeyDown(KeyCode.Keypad2))
        {   
            weaponType=1;
            playerAnimator.SetInteger("WeaponType",1);
        }
        if(Input.GetKeyDown(KeyCode.Alpha1)|| Input.GetKeyDown(KeyCode.Keypad1))
        {   
            weaponType=0;
            playerAnimator.SetInteger("WeaponType",0);
        }
        if (Input.GetMouseButtonDown(0) && weaponType==1)
        {
            playerAnimator.SetTrigger("Attack");
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        HandleInput();
        UpdateAnimation();

    }
    void UpdateAnimation()
    {
        if (playerAnimator==null) return;
        else
        {
            //lam muot
            playerAnimator.SetFloat("Speed", moveInput.magnitude*currentSpeed,0.03f,Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerAnimator.SetTrigger("Jump");
            //shadowAnimator.SetTrigger("Jump Shadow");
        }
            //Cập nhật hướng
            if (moveInput.magnitude > 0)//magnitude la do dai cua 1 vecto c^2 = a^2 +b^2
        {
            direction = isRunning ? 1 : 0.5f;// 1 là chạy , 0.5 là walk
                playerAnimator.SetFloat("InputX", moveInput.x*direction);
                playerAnimator.SetFloat("InputY", moveInput.y*direction);
                shadowAnimator.SetFloat("InputX",moveInput.x);
                shadowAnimator.SetFloat("InputY",moveInput.y);
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
            playerAnimator.SetTrigger("Dash");
            shadowAnimator.SetTrigger("Dash Shadow");
            yield return new WaitForSeconds(timeDash);
            isDashing=false;
            
        }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("death"))
        {
            //shadowTransform.position += Vector3.up*0.15f;
            playerAnimator.SetTrigger("Death");
            shadowAnimator.SetTrigger("Death Shadow");
        }
    }
}
