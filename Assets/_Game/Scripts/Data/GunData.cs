using UnityEngine;

[CreateAssetMenu(fileName = "NewGunData", menuName = "Combat/GunData")]
public class GunData : ScriptableObject
{
  public float speed;
  public float fireRate;
  public int damage;
  public float accuracy;
  public float reloadTime;
  public int magSize=30;
  public float timeReload = 1.5f;
  public float bulletLifeTime = 2f;
  public GameObject bulletPrefab;
}