using UnityEngine;

public class DestroyableCrate : MonoBehaviour
{
    public Sprite destroyedSprite;
    public GameObject foodPrefab; // Assign your Food prefab in the inspector
    public float dropDelay = 0.2f;

    private bool destroyed = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (destroyed) return;

        if (other.CompareTag("PlayerBullet")) // or Bullet, etc.
        {
            destroyed = true;
            GetComponent<SpriteRenderer>().sprite = destroyedSprite;

            // Optional: Disable the collider to prevent further triggers
            GetComponent<Collider2D>().enabled = false;

            // Drop the food
            if (foodPrefab != null)
            {
                Instantiate(foodPrefab, transform.position, Quaternion.identity);
            }

            Destroy(gameObject, dropDelay);
        }
    }
}
