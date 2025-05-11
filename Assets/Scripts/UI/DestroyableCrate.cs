using UnityEngine;

public class DestroyableCrate : MonoBehaviour
{
    public Sprite destroyedSprite;
    public GameObject[] foodPrefabs; // Assign in Inspector
    public float dropDelay = 0.2f;
    public PropSpawner spawner;
    public Vector2Int propValue;
    private void Start()
    {
        spawner = GameObject.Find("EnvironmentSpawner").GetComponent<PropSpawner>();
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("PlayerBullet"))
        {
            GetComponent<SpriteRenderer>().sprite = destroyedSprite;
            GetComponent<Collider2D>().enabled = false;

            if (foodPrefabs != null && foodPrefabs.Length > 0)
            {
                GameObject chosenPrefab = GetWeightedRandomPrefab();
                if (chosenPrefab != null)
                {
                    Instantiate(chosenPrefab, transform.position, Quaternion.identity);
                }
            }
            spawner.spawnedProps.Remove(propValue);
            Destroy(gameObject, dropDelay);
        }
    }

    private GameObject GetWeightedRandomPrefab()
    {
        // Higher weight for earlier prefabs
        int totalWeight = 0;
        int[] weights = new int[foodPrefabs.Length];

        for (int i = 0; i < foodPrefabs.Length; i++)
        {
            weights[i] = foodPrefabs.Length - i; // e.g., 3,2,1 for 3 prefabs
            totalWeight += weights[i];
        }

        int randomValue = Random.Range(0, totalWeight);
        int cumulative = 0;

        for (int i = 0; i < foodPrefabs.Length; i++)
        {
            cumulative += weights[i];
            if (randomValue < cumulative)
            {
                return foodPrefabs[i];
            }
        }

        return null; // fallback
    }
}
