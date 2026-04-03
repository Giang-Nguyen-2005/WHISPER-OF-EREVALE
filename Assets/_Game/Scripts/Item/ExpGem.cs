using UnityEngine;

public class ExpGem : MonoBehaviour, IPoolable
{
    public int expAmount = 10;
    public float moveSpeed = 5f;

    private Transform targetPlayer;
    private bool isBeingPulled = false;
    public void OnSpawn()
    {
        isBeingPulled =false;
        targetPlayer=null;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            targetPlayer=other.transform;
            isBeingPulled=true;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if(isBeingPulled && targetPlayer != null)
        {
            transform.position=Vector2.MoveTowards(transform.position,
            targetPlayer.position,moveSpeed*Time.deltaTime);
            if (Vector2.Distance(transform.position, targetPlayer.position) < 0.15f)
            {
                Collect();
            }
        }
        
    }
    private void Collect()
    {
        ExperienceManager.Instance.AddExperience(expAmount);
        gameObject.SetActive(false);
    }
}
