using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    public int animationID;
    public abstract void Attack();
    public abstract void Setup(PlayerManager player);// Kết nối với Player

    public virtual void StopAttack() { }
    public virtual void OnAnimationAttackEvent() { }

    public Sprite weaponIcon;
}