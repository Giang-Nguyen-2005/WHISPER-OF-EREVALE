using UnityEngine;

public class DebugPivot : MonoBehaviour
{
    public float size = 0.5f;
    public Color pivotColor = Color.red;

    private void OnDrawGizmos()
    {
        // Vẽ một hình cầu nhỏ ngay tại vị trí Pivot (tọa độ của GameObject)
        Gizmos.color = pivotColor;
        Gizmos.DrawWireSphere(transform.position, size);
        
        // Vẽ trục tọa độ X (đỏ) và Y (xanh lá) nhỏ tại Pivot
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * size);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.up * size);
    }
}