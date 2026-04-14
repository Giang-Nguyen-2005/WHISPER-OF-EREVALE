using UnityEngine;

[CreateAssetMenu(fileName = "NewShakeData", menuName = "Camera/Shake Data")]
public class CameraShakeData : ScriptableObject
{
    public float duration;
    public float magnitude;
}
