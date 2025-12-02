using UnityEngine;

/// <summary>
/// Helper script to fine-tune collider behavior.
/// Attach this to objects that need adjusted collision detection.
///
/// USAGE:
/// 1. Add CircleCollider2D to your GameObject
/// 2. Adjust the "Radius" to control when collision triggers:
///    - Smaller radius = circles must overlap more before collision
///    - Larger radius = circles collide when edges touch
/// 3. Use "Offset" to fine-tune the collision center
///
/// RECOMMENDED SETTINGS:
/// - For "eating" mechanic: Radius = 0.4-0.45 (requires more overlap)
/// - For edge collision: Radius = 0.5 (default, edges touch)
/// - Check "Is Trigger" for OnTriggerEnter2D (food/mass cells)
/// - Uncheck "Is Trigger" for OnCollisionEnter2D (enemies)
/// </summary>
public class ColliderHelper : MonoBehaviour
{
    [Header("Collider Settings")]
    [Tooltip("The CircleCollider2D attached to this object")]
    public CircleCollider2D circleCollider;

    [Range(0.3f, 0.5f)]
    [Tooltip("Adjust radius: smaller = more overlap needed")]
    public float colliderRadius = 0.45f;

    [Tooltip("Enable to see the actual collider size in Scene view")]
    public bool showColliderGizmo = true;

    void Start()
    {
        // Auto-find collider if not assigned
        if (!Application.isPlaying && circleCollider != null)
        {
            circleCollider = GetComponent<CircleCollider2D>();
        }

        // Apply radius setting
        if (circleCollider != null)
        {
            circleCollider.radius = colliderRadius;
        }
    }

    void OnValidate()
    {
        // Update in editor when values change
        if (circleCollider != null)
        {
            circleCollider.radius = colliderRadius;
        }
    }

    void OnDrawGizmos()
    {
        if (showColliderGizmo && circleCollider != null)
        {
            // Draw the actual collider size
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(
                transform.position,
                circleCollider.radius * transform.localScale.x
            );
        }
    }
}
