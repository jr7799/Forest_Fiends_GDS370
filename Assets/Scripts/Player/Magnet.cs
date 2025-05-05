using UnityEngine;

public class Magnet : MonoBehaviour
{
    public float magnetRadius = 5f;
    public float pullSpeed = 5f;
    public LayerMask magneticLayer;

    void Update()
    {
        // Find all nearby magnetic objects
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, magnetRadius, magneticLayer);

        foreach (Collider2D hit in hits)
        {
            Transform item = hit.transform;
            Vector2 direction = (transform.position - item.position).normalized;

            item.position += (Vector3)(direction * pullSpeed * Time.deltaTime);
        }
    }

    // Optional: draw radius in scene view
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, magnetRadius);
    }
    //public Transform target;         // Usually the player
    //public float pullSpeed = 5f;     // How fast it moves toward the player
    //public float pullRange = 5f;     // How close the player needs to be

    //void Update()
    //{
    //    if (target == null) return;

    //    float distance = Vector2.Distance(transform.position, target.position);

    //    if (distance < pullRange)
    //    {
    //        Vector2 direction = (target.position - transform.position).normalized;
    //        transform.position += (Vector3)(direction * pullSpeed * Time.deltaTime);
    //    }
    //}
}
