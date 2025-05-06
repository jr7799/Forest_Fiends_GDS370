using UnityEngine;
using System.Collections.Generic;

public class BoomerangLogic : MonoBehaviour
{
    private Transform player;
    private Vector3 moveDirection;
    private Vector3 startPos;

    private float speed;
    private float returnSpeed;
    private float maxDistance;
    private float chainRadius;
    private int maxTargets;

    private int currentTargetIndex = 0;
    private bool returning = false;
    private List<GameObject> hitEnemies = new List<GameObject>();
    private GameObject currentTarget;

    public void Initialize(
        Transform playerTransform,
        float speed,
        float returnSpeed,
        float maxDistance,
        float chainRadius,
        int maxTargets,
        Vector3 initialDirection)
    {
        this.player = playerTransform;
        this.speed = speed;
        this.returnSpeed = returnSpeed;
        this.maxDistance = maxDistance;
        this.chainRadius = chainRadius;
        this.maxTargets = maxTargets;
        this.moveDirection = initialDirection.normalized;
        this.startPos = transform.position;
    }

    void Update()
    {
        if (player == null) return;

        // Constantly rotate the boomerang sprite
        transform.Rotate(0f, 0f, 720f * Time.deltaTime); // Spin clockwise (use -720f to spin counterclockwise)

        if (!returning)
        {
            if (currentTarget != null)
            {
                // Move toward next enemy target
                Vector3 dir = (currentTarget.transform.position - transform.position).normalized;
                transform.position += dir * speed * Time.deltaTime;

                if (Vector3.Distance(transform.position, currentTarget.transform.position) < 0.2f)
                {
                    ChainToNextEnemy(currentTarget);
                }
            }
            else
            {
                // No target yet, just fly forward
                transform.position += moveDirection * speed * Time.deltaTime;

                if (Vector3.Distance(transform.position, startPos) >= maxDistance)
                {
                    returning = true;
                }
            }
        }
        else
        {
            Vector3 dir = (player.position - transform.position).normalized;
            transform.position += dir * returnSpeed * Time.deltaTime;

            if (Vector3.Distance(transform.position, player.position) < 0.2f)
            {
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!returning && other.CompareTag("Enemy") && !hitEnemies.Contains(other.gameObject))
        {
            ChainToNextEnemy(other.gameObject);
        }
    }

    void ChainToNextEnemy(GameObject justHit)
    {
        hitEnemies.Add(justHit);

        if (hitEnemies.Count >= maxTargets)
        {
            returning = true;
            return;
        }

        GameObject nextTarget = FindNextEnemy(justHit.transform.position);

        if (nextTarget != null)
        {
            currentTarget = nextTarget;
        }
        else
        {
            returning = true;
        }
    }

    GameObject FindNextEnemy(Vector3 fromPos)
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(fromPos, chainRadius);
        GameObject closest = null;
        float minDist = Mathf.Infinity;

        foreach (var hit in hits)
        {
            if (hit.CompareTag("Enemy") && !hitEnemies.Contains(hit.gameObject))
            {
                float dist = Vector3.Distance(fromPos, hit.transform.position);
                if (dist < minDist)
                {
                    closest = hit.gameObject;
                    minDist = dist;
                }
            }
        }

        return closest;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chainRadius);
    }
}
