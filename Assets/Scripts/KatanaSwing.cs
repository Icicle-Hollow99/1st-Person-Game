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
        if (Input.GetMouseButtonDown(0) && !isSwinging) // Left mouse button to swing
        {
            StartCoroutine(Swing());
        }
    }

    IEnumerator Swing()
    {
        isSwinging = true;
        float elapsedTime = 0f;

        while (elapsedTime < 1f / swingSpeed)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime * swingSpeed);

            if (swingForward)
            {
                transform.localRotation = Quaternion.Slerp(initialRotation, targetRotation, t);
            }
            else
            {
                transform.localRotation = Quaternion.Slerp(targetRotation, initialRotation, t);
            }

            yield return null;
        }

        swingForward = !swingForward;
        isSwinging = false;
    }
}