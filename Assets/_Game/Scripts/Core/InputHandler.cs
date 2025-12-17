using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public Vector2 moveInput;
    public bool isRunning;
    public bool isDashKeyDown;
    public bool isJumpKeyDown;
    public bool isAttackKeyDown;
    public bool isWeapon1KeyDown;
    public bool isWeapon2KeyDown;

    void Update()
    {
        float moveY = Input.GetAxisRaw("Vertical");
        float moveX = Input.GetAxisRaw("Horizontal");
        moveInput = new Vector2(moveX, moveY).normalized;

        if (Input.GetKey(KeyCode.LeftShift)) isRunning = true;
        else isRunning = false;

        isDashKeyDown = Input.GetKeyDown(KeyCode.Z);
        isJumpKeyDown = Input.GetKeyDown(KeyCode.Space);
        
        isAttackKeyDown = Input.GetMouseButtonDown(0);
        
        if(Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
        {
            isWeapon1KeyDown=true;
            isWeapon2KeyDown=false;
        }
        if(Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
        {
            isWeapon2KeyDown=true;
            isWeapon1KeyDown=false;
        }
    }
}