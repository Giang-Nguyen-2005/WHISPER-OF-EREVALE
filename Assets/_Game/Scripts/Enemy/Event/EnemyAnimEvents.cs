using UnityEngine;

public class EnemyAnimEvents : MonoBehaviour
{
    private EnemyBase enemyLogic;

    void Awake()
    {
        enemyLogic = GetComponentInParent<EnemyBase>();
    }
    
    public void TriggerAttack()
    {
        if (enemyLogic != null)
        {
            enemyLogic.PerformAttackLogic();
        }
    }
}