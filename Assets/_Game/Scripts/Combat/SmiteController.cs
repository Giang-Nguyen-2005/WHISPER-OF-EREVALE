using UnityEngine;
using System.Collections.Generic;

public class SmiteController : MonoBehaviour
{
    [Header("Setup")]
    public GameObject smitePrefab;
    public LayerMask enemyLayer;

    [Header("Stats")]
    public bool isUnlocked = false; 
    public float cooldown = 3f; 
    public int baseDamage = 50; 
    public int strikeCount = 1;
    public bool isChainLightning = false;

    private float nextSmiteTime;
    private PlayerManager player;

    void Start()
    {
        player = GetComponent<PlayerManager>();
    }

    void Update()
    {
        if (!isUnlocked) return;

        if (Time.time >= nextSmiteTime)
        {
            CastSmite();
            nextSmiteTime = Time.time + cooldown;
        }
    }

    private void CastSmite()
    {
        // Tìm trong 10m
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, 10f, enemyLayer);
        
        if (hitEnemies.Length == 0) return; // return

        List<Collider2D> targetList = new List<Collider2D>(hitEnemies);
        int hitsToMake = Mathf.Min(strikeCount, targetList.Count); 

        for (int i = 0; i < hitsToMake; i++)
        {
            int randomIndex = Random.Range(0, targetList.Count);
            Transform targetTransform = targetList[randomIndex].transform;
            targetList.RemoveAt(randomIndex); 


            GameObject smite = Instantiate(smitePrefab, targetTransform.position + Vector3.up * 0.5f, Quaternion.identity);
            smite.GetComponent<SmiteDamage>().Init(baseDamage, player);


            if (isChainLightning)
            {
               //todo
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 10f);
    }
}