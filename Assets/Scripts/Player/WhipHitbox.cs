using UnityEngine;

public class WhipHitbox : MonoBehaviour
{
    public float damage = 10f;

    private void OnEnable()
    {
        Invoke(nameof(Disable), 0.2f); 
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyHealth enemy = other.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage, false);
            }
        }
    }

    void Disable()
    {
        gameObject.SetActive(false);
    }
}
