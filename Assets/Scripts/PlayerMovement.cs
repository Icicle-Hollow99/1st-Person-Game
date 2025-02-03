using UnityEngine;
using UnityEngine.SceneManagement; // Required for scene management

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float sprintSpeed = 8f; // Sprint speed when Shift is held
    public float jumpForce = 5f; // Jump force
    private bool isGrounded; // To check if the player is on the ground
    private Rigidbody rb;

    // Mouse look sensitivity
    public float lookSpeedX = 2f;
    public float lookSpeedY = 2f;
    private float rotationX = 0f; // Store the vertical rotation

    private bool isSprinting = false; // Track if player is sprinting

    // Assign this in the inspector
    public Texture2D dotCursor;  // The tiny dot texture

    private Camera playerCamera;

    // Health variables
    public float maxHealth = 100f; // Maximum health
    private float currentHealth;   // Current health

    // Weapon switching variables
    public GameObject katana; // Reference to the katana GameObject
    public GameObject gun; // Reference to the gun GameObject

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Get the Rigidbody component
        playerCamera = Camera.main; // Reference to the main camera

        // Lock the cursor and hide it from the screen
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;  // Make sure the cursor is visible (crosshair should show)
        
        // Set the custom crosshair texture
        Cursor.SetCursor(dotCursor, Vector2.zero, CursorMode.ForceSoftware);

        // Initialize health
        currentHealth = maxHealth;

        // Ensure the katana is active and the gun is hidden at the start
        katana.SetActive(true);
        gun.SetActive(false);
    }

    void Update()
    {
        // Check for Sprinting (holding Shift)
        isSprinting = Input.GetKey(KeyCode.LeftShift);

        // Player movement (depending on Sprinting)
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        float currentSpeed = isSprinting ? sprintSpeed : speed; // Use sprintSpeed if Shift is held
        Vector3 move = new Vector3(moveX, 0, moveZ) * currentSpeed * Time.deltaTime;
        transform.Translate(move);

        // Jumping logic
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); // Apply a force upward
        }

        // Mouse look (horizontal and vertical)
        float mouseX = Input.GetAxis("Mouse X") * lookSpeedX;
        float mouseY = Input.GetAxis("Mouse Y") * lookSpeedY;

        // Rotate the player left/right (around Y-axis)
        transform.Rotate(0, mouseX, 0);

        // Rotate the camera up/down (around X-axis)
        rotationX -= mouseY;
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);

        // Weapon switching logic
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchToKatana();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchToGun();
        }
    }

    private void SwitchToKatana()
    {
        katana.SetActive(true);
        gun.SetActive(false);
        Debug.Log("Switched to katana");
    }

    private void SwitchToGun()
    {
        katana.SetActive(false);
        gun.SetActive(true);
        Debug.Log("Switched to gun");
    }

    private void OnCollisionStay(Collision other)
    {
        // Check if the player is touching the ground
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        // Set isGrounded to false when not touching the ground
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    // Method to handle taking damage
    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Method to handle death (restart the scene)
    private void Die()
    {
        Debug.Log("Player has died! Restarting scene...");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Restart scene
    }

    // Method to heal the player (optional)
    public void Heal(float healAmount)
    {
        currentHealth = Mathf.Min(currentHealth + healAmount, maxHealth); // Ensure health doesn't exceed max
    }
}