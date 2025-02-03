using UnityEngine;

public class GunScript : MonoBehaviour
{
    public float fireRate = 0.5f; // Time between shots
    public float damage = 50f; // Damage per shot
    public float range = 100f; // Range of the gun
    public Camera playerCamera; // Reference to the player's camera

    private float nextTimeToFire = 0f;

    void Update()
    {
        if (gameObject.activeInHierarchy && Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        // Perform a raycast from the center of the camera
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, range))
        {
            Debug.Log("Hit: " + hit.transform.name);

            // Check if the hit object is an enemy
            EnemyBehavior enemy = hit.transform.GetComponent<EnemyBehavior>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                Debug.Log("Enemy hit and took damage!");
            }
        }
    }
}