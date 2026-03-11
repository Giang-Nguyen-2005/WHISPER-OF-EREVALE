using UnityEditor.Build.Content;
using UnityEngine;

public class PlayerAnimationEvent : MonoBehaviour
{
    private PlayerManager player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player=GetComponentInParent<PlayerManager>();   
    }

    public void AttackAnimationTrigger()
    {
        if (player != null)
        {
            player.combat.SpearAttack();
        }
    }
}
