using UnityEngine;

[CreateAssetMenu(fileName = "NewGunData", menuName = "Combat/GunData")]
public class GunData : ScriptableObject
{
  public float fireRate;
  public int damage;
  public float accuracy;
  public float reloadTime;
  public GameObject bulletPrefab;
}