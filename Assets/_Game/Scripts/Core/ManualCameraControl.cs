using UnityEngine;

public class ManualCameraControl : MonoBehaviour
{
    [Header("Target Settings")]
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Vector3 offset = new Vector3(0, 0, -10f);

    [Header("Movement Settings")]
    [SerializeField] private float smoothTime = 0.15f; // Càng nhỏ càng bám sát
    private Vector3 currentVelocity = Vector3.zero;

    [Header("Mouse Look-ahead")]
    [SerializeField] private float mouseInfluence = 0.02f; // Độ lệch theo hướng chuột
    [SerializeField] private Camera mainCam;

    private CameraShakeData currentShake;

    [Header("Shake Settings")]
    private float shakeDuration = 0f;
    private float shakeMagnitude = 0.1f;
    private Vector3 currentShakeOffset;

    public static ManualCameraControl Instance;

    private void Awake() => Instance = this;

    void OnEnable() => PlayerEvents.OnPlayerHit += RequestShake;
    void OnDisable() => PlayerEvents.OnPlayerHit -= RequestShake;
    private void LateUpdate()
    {
        if (playerTransform == null) return;

        Vector3 targetPos = playerTransform.position + offset;

        // Mouse Look-ahead
        Vector3 mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 directionToMouse = (mousePos - playerTransform.position).normalized;
        float distanceToMouse = Vector2.Distance(playerTransform.position, mousePos);

        // Giới hạn tầm nhìn chuột để camera không bay quá xa
        float clampedDistance = Mathf.Clamp(distanceToMouse, 0f, mouseInfluence);
        //Mathf.Clamp(value, min, max) ==> min <=value <=max

        targetPos += directionToMouse * clampedDistance;

        // Shake
        HandleShake();

        // SmoothDamp
        Vector3 finalPos = Vector3.SmoothDamp(transform.position, targetPos, ref currentVelocity, smoothTime);
        transform.position = finalPos + currentShakeOffset;
    }
    public void RequestShake(CameraShakeData data)
    {
        if (data == null) return;
        currentShake = data;
        shakeDuration = data.duration;
        shakeMagnitude = data.magnitude;
    }

    private void HandleShake()
    {
        if (shakeDuration > 0)
        {
            currentShakeOffset = Random.insideUnitSphere * shakeMagnitude;
            currentShakeOffset.z = 0;
            shakeDuration -= Time.deltaTime;
        }
        else
        {
            currentShakeOffset = Vector3.zero;
        }
    }
}