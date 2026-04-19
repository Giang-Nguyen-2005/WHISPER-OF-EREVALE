using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public Vector2 moveInput;
    public Vector2 mouseWorldPosition;
    
    [Header("Skill Inputs")]
    public bool isRunSkillDown;
    public bool isDashKeyDown;
    public bool isAttackKeyDown;

    [Header("Weapon Switch")]
    public bool isWeapon1KeyDown;
    public bool isWeapon2KeyDown;
    public bool isWeapon3KeyDown;

    void Update()
    {
        float moveY = Input.GetAxisRaw("Vertical");
        float moveX = Input.GetAxisRaw("Horizontal");
        moveInput = new Vector2(moveX, moveY).normalized;

        if (Camera.main != null)
        {
            mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        isRunSkillDown = Input.GetKeyDown(KeyCode.Z); 
        isDashKeyDown = Input.GetKeyDown(KeyCode.Space);
        isAttackKeyDown = Input.GetMouseButtonDown(0);

        HandleWeaponInput();
    }

    private void HandleWeaponInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1)) SetWeapon(1);
        else if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2)) SetWeapon(2);
        else if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3)) SetWeapon(3);
        else 
        {
            isWeapon1KeyDown = false;
            isWeapon2KeyDown = false;
            isWeapon3KeyDown = false;
        }
    }

    private void SetWeapon(int id)
    {
        isWeapon1KeyDown = (id == 1);
        isWeapon2KeyDown = (id == 2);
        isWeapon3KeyDown = (id == 3);
    }
}