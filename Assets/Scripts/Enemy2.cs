using UnityEngine;

public class EnemyBounce : MonoBehaviour
{
    public float speed = 3f; // Movement speed
    public Rigidbody rb; // Reference to the Rigidbody component

    void Start()
    {
        if (rb == null)
        {
            Debug.LogError("Rigidbody component is missing!");
            return;
        }

        // Ensure Rigidbody is not kinematic and gravity is enabled
        rb.isKinematic = false;
        rb.useGravity = true;

        // Apply initial force to ensure movement
        rb.AddForce(transform.forward * speed, ForceMode.VelocityChange);
        Debug.Log("Enemy started moving with velocity: " + rb.linearVelocity);
    }

    void Update()
    {
        Debug.Log("Current velocity: " + rb.linearVelocity);
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision detected with: " + collision.gameObject.name);
        
        // Reverse direction on hitting a wall
        if (collision.gameObject.CompareTag("Wall"))
        {
            rb.linearVelocity = new Vector3(-rb.linearVelocity.x, rb.linearVelocity.y, rb.linearVelocity.z);
            Debug.Log("Enemy direction reversed. New velocity: " + rb.linearVelocity);
        }
    }
}