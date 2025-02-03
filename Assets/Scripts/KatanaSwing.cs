using UnityEngine;
using System.Collections;

public class KatanaSwing : MonoBehaviour
{
    public float swingSpeed = 15f; // Speed of the swing
    private bool isSwinging = false;
    private bool swingForward = true; // Track swing direction
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

        // Set the target rotation for swinging
        targetRotation = initialRotation * Quaternion.Euler(new Vector3(0, 0, 90f)); // Example swing angle
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

        float elapsedTime = 0f;
        float duration = 1f / swingSpeed;

        Quaternion startRotation = swingForward ? initialRotation : targetRotation;
        Quaternion endRotation = swingForward ? targetRotation : initialRotation;

        // Swing to target position and rotation
        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            transform.localRotation = Quaternion.Lerp(startRotation, endRotation, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.localRotation = endRotation; // Ensure exact rotation

        swingForward = !swingForward; // Toggle swing direction
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
