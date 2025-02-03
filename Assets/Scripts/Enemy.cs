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
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Enemy hit the player!"); // Should appear in console
            PlayerMovement playerMovement = collision.gameObject.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                Debug.Log("Player should take damage now!");
                playerMovement.TakeDamage(damage);
            }
        }
    }

    // This method will be called when the katana or gun hits the enemy
    public void TakeHit()
    {
        hitCount++;

        // If the enemy has been hit the max number of times, destroy the enemy
        if (hitCount >= maxHits)
        {
            Debug.Log("Enemy has been defeated!");
            Destroy(gameObject); // Destroy the enemy GameObject
        }
    }

    // Method to handle taking damage from the gun
    public void TakeDamage(float damage)
    {
        hitCount++;

        // If the enemy has been hit the max number of times, destroy the enemy
        if (hitCount >= maxHits)
        {
            Debug.Log("Enemy has been defeated!");
            Destroy(gameObject); // Destroy the enemy GameObject
        }
    }
}