using UnityEngine;

public class Throwable : MonoBehaviour
{
    public GameObject potionPrefab;
    public float throwForce = 10f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J)) // Press J to throw
        {
            ThrowPotion();
        }
    }

    void ThrowPotion()
    {
        Debug.Log("ThrowPotion triggered");
        if (potionPrefab == null)
        {
            Debug.LogWarning("Missing reference for potionPrefab.");
            return;
        }

        Vector2 throwDirection = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;

        GameObject potion = Instantiate(potionPrefab, transform.position, Quaternion.identity);
        Rigidbody2D rb = potion.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.AddForce(throwDirection * throwForce, ForceMode2D.Impulse);
        }
    }
}
