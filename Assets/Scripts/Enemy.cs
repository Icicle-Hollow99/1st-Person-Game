using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public float moveSpeed = 5f; // Movement speed
    public float damage = 10f;   // Damage to the player
    public int maxHits = 2;      // Maximum number of hits before the enemy disappears
    private int hitCount = 0;    // Counter to track how many times the enemy has been hit

    private Transform player;    // Reference to the player's transform

    void Start()
    {
        // Find the player object in the scene
        player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        // Move the enemy toward the player
        if (player != null)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        PlayerMovement playerMovement = collision.gameObject.GetComponent<PlayerMovement>();
        if (playerMovement != null)
        {
            playerMovement.TakeDamage(damage);
            hitCount++;
            if (hitCount >= maxHits)
            {
                Destroy(gameObject); // Destroy the enemy after max hits
            }
        }
    }

    public void TakeDamage(float damage)
    {
        // Implement the logic for taking damage
        hitCount++;
        if (hitCount >= maxHits)
        {
            Destroy(gameObject); // Destroy the enemy after max hits
        }
    }
}