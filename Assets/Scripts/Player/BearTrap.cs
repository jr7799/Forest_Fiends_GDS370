using UnityEngine;

public class BearTrap : MonoBehaviour
{
    public float throwForce = 10f;

    public void ThrowTrap(Vector2 direction)
    {
        if (direction == Vector2.zero) return;

        direction = GetCardinalDirection(direction);

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero; // Clear existing velocity
            rb.AddForce(direction * throwForce, ForceMode2D.Impulse);
        }
    }

    Vector2 GetCardinalDirection(Vector2 input)
    {
        // Pick the dominant axis and zero out the other
        if (Mathf.Abs(input.x) > Mathf.Abs(input.y))
            return new Vector2(Mathf.Sign(input.x), 0);
        else
            return new Vector2(0, Mathf.Sign(input.y));
    }
}
