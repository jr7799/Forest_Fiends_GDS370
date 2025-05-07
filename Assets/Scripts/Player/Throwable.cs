using UnityEngine;

public class Throwable : MonoBehaviour
{
    public GameObject potionPrefab;
    public float throwForce = 10f;


    public void ThrowPotionExternally(Vector2 direction)
    {
        if (potionPrefab == null)
        {
            Debug.LogWarning("Missing reference for potionPrefab.");
            return;
        }

        GameObject potion = Instantiate(potionPrefab, transform.position, Quaternion.identity);
        Rigidbody2D rb = potion.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.AddForce(direction * throwForce, ForceMode2D.Impulse);
        }
    }
}
