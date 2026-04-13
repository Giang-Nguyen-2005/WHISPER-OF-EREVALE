using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InGameHUD : MonoBehaviour
{
    [Header("Health UI")]
    [SerializeField] private Slider healthSlider;

    [Header("Ammo UI")]
    [SerializeField] private GameObject ammoContainer;
    [SerializeField] private TextMeshProUGUI ammoText;

    [Header("Weapon UI")]
    [SerializeField] private Image weaponIconImage;

    private void OnEnable() {
        PlayerEvents.OnHealthChanged += UpdateHealth;
        PlayerEvents.OnAmmoChanged += UpdateAmmo;
        PlayerEvents.OnWeaponChanged += UpdateWeaponUI;
    }

    private void OnDisable() {
        PlayerEvents.OnHealthChanged -= UpdateHealth;
        PlayerEvents.OnAmmoChanged -= UpdateAmmo;
        PlayerEvents.OnWeaponChanged -= UpdateWeaponUI;
    }

    private void UpdateHealth(int current, int max) {
        healthSlider.maxValue = max;
        healthSlider.value = current;
    }

    private void UpdateAmmo(int current, int max) {
        ammoText.text = $"{current} / {max}";
    }

    private void UpdateWeaponUI(WeaponBase weapon) {
        if (weapon == null) {
            weaponIconImage.enabled = false;
            ammoContainer.SetActive(false);
            return;
        }

        weaponIconImage.enabled = true;
        weaponIconImage.sprite = weapon.weaponIcon;
        ammoContainer.SetActive(weapon is GunWeapon);
    }
}