using UnityEngine;

public class GunScript : MonoBehaviour
{
    public float fireRate = 0.5f;
    public float damage = 50f;
    public float range = 100f;
    public Camera playerCamera;
    public GameObject bulletPrefab; // Reference to the Bullet Prefab
    public Transform bulletSpawnPoint; // Where the bullet will spawn

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
        // Instantiate and fire the bullet
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        BulletScript bulletScript = bullet.GetComponent<BulletScript>();
        if (bulletScript != null)
        {
            bulletScript.speed = 20f;
        }

        // Perform a raycast from the center of the camera
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, range))
        {
            Debug.Log("Hit: " + hit.transform.name);

            EnemyBehavior enemy = hit.transform.GetComponent<EnemyBehavior>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                Debug.Log("Enemy hit and took damage!");
            }
        }
    }
}