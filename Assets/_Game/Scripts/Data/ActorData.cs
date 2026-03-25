using UnityEngine;

[CreateAssetMenu(fileName = "NewActorData", menuName = "Combat/ActorData")]
public class ActorData : ScriptableObject
{
public string actorName;
public int maxHealth;
public float baseSpeed;
}
