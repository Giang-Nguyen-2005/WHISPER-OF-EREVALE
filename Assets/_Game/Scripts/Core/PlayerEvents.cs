using System;

public static class PlayerEvents
{
    public static Action<int,int> OnHealthChanged; // CurrentHealt, MaxHealth
    public static Action OnPlayerDeath;

    public static Action<int,int> OnExperienceChanged;
    public static Action<int> OnLevelUp;

    public static Action<int,int> OnAmmoChanged;
    public static Action<WeaponBase> OnWeaponChanged;
}