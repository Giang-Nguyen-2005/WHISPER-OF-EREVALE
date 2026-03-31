using TMPro;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public WeaponBase currentWeapon;
    public WeaponBase spearWeapon;
    public WeaponBase gunWeapon;

    private PlayerManager player;

    void Start() => player = GetComponent<PlayerManager>();

    void Update()
    {
        // 1. Đổi vũ khí dựa trên phím bấm từ InputHandler
        if (player.inputHandler.isWeapon1KeyDown) SwitchWeapon(null); // Tay không (animationID = 0)
        if (player.inputHandler.isWeapon2KeyDown) SwitchWeapon(spearWeapon); // Giáo (animationID = 1)
        if (player.inputHandler.isWeapon3KeyDown) SwitchWeapon(gunWeapon);   // Súng (animationID = 3)

        bool isHoldingMouse = Input.GetMouseButton(0);
        if (currentWeapon != null)
        {
            if (isHoldingMouse)
            {
                currentWeapon.Attack();
            }
            else
            {
                currentWeapon.StopAttack();
            }
        }
    }

   public void SwitchWeapon(WeaponBase newWeapon)
{
    if (currentWeapon == newWeapon) return; 

    currentWeapon = newWeapon;

    if (currentWeapon != null)
    {
        currentWeapon.Setup(player);
        player.anim.UpdateWeaponAnimation(currentWeapon.animationID);
    }
    else
    {
        player.anim.UpdateWeaponAnimation(0);
    }
}
}