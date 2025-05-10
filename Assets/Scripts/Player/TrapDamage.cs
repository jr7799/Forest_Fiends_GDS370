using UnityEngine;

public class TrapDamage : MonoBehaviour
{
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
