using UnityEngine;

public class DestroyableCrate : MonoBehaviour
{
    public Sprite destroyedSprite;
    public GameObject[] foodPrefabs; // Assign two or more prefabs in the Inspector
    public float dropDelay = 0.2f;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("PlayerBullet")) // or Bullet, etc.
        {
            GetComponent<SpriteRenderer>().sprite = destroyedSprite;

            // Optional: Disable the collider to prevent further triggers
            GetComponent<Collider2D>().enabled = false;

            // Drop a random food prefab
            if (foodPrefabs != null && foodPrefabs.Length > 0)
            {
                int randomIndex = Random.Range(0, foodPrefabs.Length);
                Instantiate(foodPrefabs[randomIndex], transform.position, Quaternion.identity);
            }

            Destroy(gameObject, dropDelay);
        }
    }
}
