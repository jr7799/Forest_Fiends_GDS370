using UnityEngine;
using System.Collections;

public class LobArcTween2D : MonoBehaviour
{
    public float lobRadius = 4f;
    public float arcHeight = 2f;
    public float duration = 0.6f;

    private Vector2 startPoint;
    private Vector2 targetPoint;

    void Start()
    {
        // Pick random target point in radius
        startPoint = transform.position;
        Vector2 offset = Random.insideUnitCircle.normalized * Random.Range(1f, lobRadius);
        targetPoint = startPoint + offset;

        // Start arc movement
        StartCoroutine(ArcMove());
        Destroy(gameObject, Random.Range(10, 15));
    }

    IEnumerator ArcMove()
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float t = elapsed / duration;

            // Basic lerp between start and end
            Vector2 linearPos = Vector2.Lerp(startPoint, targetPoint, t);

            // Apply arc height (parabolic curve)
            float height = 4 * arcHeight * t * (1 - t); // Parabola: peak at t=0.5
            Vector2 arcPos = linearPos + Vector2.up * height;

            transform.position = arcPos;

            elapsed += Time.deltaTime;
            yield return null;
        }

        // Final position snap (just in case)
        transform.position = targetPoint;
    }

    public float damage = 5;
    [SerializeField] bool isCriticalHit;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        isCriticalHit = Random.Range(0, 100) < 30;
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyHealth enemy = collision.gameObject.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                if (!isCriticalHit)
                    enemy.TakeDamage(damage, false);
                else
                    enemy.TakeDamage(damage * 1.35f, true);
            }
            Destroy(gameObject);
        }
    }
}