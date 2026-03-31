using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayerData", menuName = "Combat/PlayerData")]
public class PlayerData : ActorData
{
    [Header("Movement Settings")]
    public float walkSpeed = 1.25f;
    public float runSpeed = 2.2f;
    public float dashSpeed = 5.2f;
    public float dashDuration = 0.18f;

    [Header("Survival Settings")]
    public float invincibilityDuration = 1f;
}