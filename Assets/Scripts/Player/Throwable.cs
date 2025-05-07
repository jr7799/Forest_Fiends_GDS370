using UnityEngine;

public class Throwable : MonoBehaviour
{
    public GameObject potionPrefab;
    public Transform tempPlayerWeapon;
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
        if (tempPlayerWeapon == null || potionPrefab == null)
        {
            Debug.LogWarning("Missing reference for potionPrefab or tempPlayerWeapon.");
            return;
        }

        Vector2 throwDirection = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - tempPlayerWeapon.position).normalized;

        GameObject potion = Instantiate(potionPrefab, tempPlayerWeapon.position, Quaternion.identity);
        Rigidbody2D rb = potion.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.AddForce(throwDirection * throwForce, ForceMode2D.Impulse);
        }
    }
}
