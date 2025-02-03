using UnityEngine;
using System.Collections;

public class KatanaSwing : MonoBehaviour
{
    public float swingSpeed = 15f; // Speed of the swing
    private bool isSwinging = false;
    private Vector3 initialPosition; // Original position
    private Quaternion initialRotation; // Original rotation
    private Vector3 targetPosition; // Target position for the swing
    private Quaternion targetRotation; // Target rotation for the swing

    // Reference to the katana collider
    public Collider katanaCollider; 

    void Start()
    {
        initialPosition = transform.localPosition; // Save the starting position
        initialRotation = transform.localRotation; // Save the starting rotation
        if (katanaCollider == null)
        {
            katanaCollider = GetComponent<Collider>(); // Ensure we have a collider reference
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isSwinging) // When left-click is pressed
        {
            StartCoroutine(SwingKatana());
        }
    }

    private IEnumerator SwingKatana()
    {
        isSwinging = true;

        // Generate random target position and rotation for the swing
        targetPosition = initialPosition + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0);
        targetRotation = initialRotation * Quaternion.Euler(Random.Range(-30f, 30f), Random.Range(-30f, 30f), 0);

        float elapsedTime = 0f;
        float duration = 1f / swingSpeed;

        // Swing to target position and rotation
        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            transform.localPosition = Vector3.Lerp(initialPosition, targetPosition, t);
            transform.localRotation = Quaternion.Lerp(initialRotation, targetRotation, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = targetPosition; // Ensure exact position
        transform.localRotation = targetRotation; // Ensure exact rotation

        yield return new WaitForSeconds(0.1f); // **Pause at full swing**

        // Return to original position and rotation
        elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            transform.localPosition = Vector3.Lerp(targetPosition, initialPosition, t);
            transform.localRotation = Quaternion.Lerp(targetRotation, initialRotation, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = initialPosition; // Ensure exact position
        transform.localRotation = initialRotation; // Ensure exact rotation

        isSwinging = false;
    }

    // Detect when the katana collider hits another collider (trigger)
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Katana hit an enemy!"); // Debug log to confirm hit

            EnemyBehavior enemy = other.GetComponent<EnemyBehavior>();
            if (enemy != null)
            {
                enemy.TakeHit(); // Call the TakeHit method to damage the enemy
                Debug.Log("Enemy has been hit!");
            }
        }
    }
}
