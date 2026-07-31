using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifeTime = 3f;
    public float damage = 10f;
    public float critMult = 1.35f;
    public float critChance = 2;
    [SerializeField] bool isCriticalHit;
    public GameObject bulletPop;
    SoundManager soundManager;
    void Start()
    {
        isCriticalHit = Random.Range(0, 100) < critChance;
        //soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        soundManager = SoundManager.instance;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                if(!isCriticalHit)
                    enemyHealth.TakeDamage(damage, false);
                else
                    enemyHealth.TakeDamage(damage * 1.35f, true);
            }
            Destroy(gameObject); // Destroy bullet on impact
        }
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        Instantiate(GameAssets.i.bulletPop, transform.position, Quaternion.identity);
    }
}
