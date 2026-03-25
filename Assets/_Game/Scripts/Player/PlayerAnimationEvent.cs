using UnityEngine;

public class PlayerAnimationEvent : MonoBehaviour
{
    private PlayerManager player;

    void Start()
    {
        player = GetComponentInParent<PlayerManager>();   
    }

    public void AttackAnimationTrigger()
    {
        if (player != null && player.combat.currentWeapon != null)
        {
            player.combat.currentWeapon.OnAnimationAttackEvent();
        }
    }
}