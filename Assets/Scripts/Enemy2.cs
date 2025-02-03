using UnityEngine;

public class EnemyBounce : MonoBehaviour
{
    public float speed = 3f; // Movement speed
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = new Vector2(speed, 0); // Start moving right
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Reverse direction on hitting a wall
        if (collision.gameObject.CompareTag("Wall"))
        {
            rb.linearVelocity = new Vector2(-rb.linearVelocity.x, rb.linearVelocity.y);
        }
    }
}
