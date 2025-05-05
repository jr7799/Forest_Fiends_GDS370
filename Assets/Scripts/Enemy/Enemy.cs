using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 3f;
    public float avoidDistance = 1f;
    public float detectionRange = 1.5f;
    public LayerMask obstacleLayer;

    public bool isEye = false;
    SpriteRenderer sprite;
    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        if (player == null)
        {
            GameObject playerObj = GameObject.Find("Player");
            if (playerObj != null)
                player = playerObj.transform;
        }
    }

    private void Update()
    {
        if (player == null) return;

        Vector2 direction = (player.position - transform.position).normalized;

        // Check for obstacle in front
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, detectionRange, obstacleLayer);
        if (hit.collider != null)
        {
            // Try offsetting to the side if blocked
            Vector2 perp = Vector2.Perpendicular(direction).normalized;

            RaycastHit2D left = Physics2D.Raycast(transform.position, -perp, detectionRange, obstacleLayer);
            RaycastHit2D right = Physics2D.Raycast(transform.position, perp, detectionRange, obstacleLayer);

            if (right.collider == null)
                direction = perp;
            else if (left.collider == null)
                direction = -perp;
            else
                direction = -direction; // fully blocked — move back a bit
        }
        if (player.position.x < transform.position.x)
        {
            sprite.flipX = false; // Face right
        }
        else
        {
            sprite.flipX = true; // Face left
        }
        transform.position += (Vector3)(direction * moveSpeed * Time.deltaTime);
    }
}
