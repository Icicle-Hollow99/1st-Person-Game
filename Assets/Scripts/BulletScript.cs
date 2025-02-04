using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float speed = 20f;
    public float lifeTime = 2f;

    void Start()
    {
        // Destroy the bullet after lifeTime seconds
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        // Move the bullet forward
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        // Handle collision with other objects
        Destroy(gameObject); // Destroy the bullet on collision
    }
}