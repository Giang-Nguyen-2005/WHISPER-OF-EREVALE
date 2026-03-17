using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public Vector2 moveInput;
    public Vector2 mouseWorldPosition;
    public bool isRunning;
    public bool isDashKeyDown;
    public bool isJumpKeyDown;
    public bool isAttackKeyDown;
    public bool isWeapon1KeyDown;
    public bool isWeapon2KeyDown;
    public bool isWeapon3KeyDown;

    void Update()
    {
        float moveY = Input.GetAxisRaw("Vertical");
        float moveX = Input.GetAxisRaw("Horizontal");
        moveInput = new Vector2(moveX, moveY).normalized;

        // Cập nhật vị trí chuột
        mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        isRunning = Input.GetKey(KeyCode.LeftShift);
        isDashKeyDown = Input.GetKeyDown(KeyCode.Z);
        isJumpKeyDown = Input.GetKeyDown(KeyCode.Space);
        isAttackKeyDown = Input.GetMouseButtonDown(0);

        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1)) SetWeapon(1);
        if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2)) SetWeapon(2);
        if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3)) SetWeapon(3);
    }

    private void SetWeapon(int id)
    {
        isWeapon1KeyDown = (id == 1);
        isWeapon2KeyDown = (id == 2);
        isWeapon3KeyDown = (id == 3);
    }
}