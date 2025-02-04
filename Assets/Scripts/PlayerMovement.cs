using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float sprintSpeed = 8f;
    public float jumpForce = 5f;
    private bool isGrounded;
    private Rigidbody rb;

    public float lookSpeedX = 2f;
    public float lookSpeedY = 2f;
    private float rotationX = 0f;

    public Texture2D dotCursor;

    private Camera playerCamera;

    public float maxHealth = 100f;
    private float currentHealth;

    // Weapon management
    public GameObject katana;
    public GameObject glockG22;
    private int selectedWeaponIndex = 0;
    private WeaponUIManager weaponUIManager;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerCamera = Camera.main;
        Cursor.SetCursor(dotCursor, Vector2.zero, CursorMode.Auto);

        weaponUIManager = Object.FindFirstObjectByType<WeaponUIManager>();
        currentHealth = maxHealth;
        UpdateWeaponUI();

        // Lock the cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        HandleMovement();
        HandleWeaponSwap();

        // Unlock the cursor when the Escape key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    void HandleMovement()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        movement = transform.TransformDirection(movement);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            rb.linearVelocity = movement * sprintSpeed;
        }
        else
        {
            rb.linearVelocity = movement * speed;
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        float mouseX = Input.GetAxis("Mouse X") * lookSpeedX;
        float mouseY = Input.GetAxis("Mouse Y") * lookSpeedY;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);

        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    void HandleWeaponSwap()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) SelectWeapon(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) SelectWeapon(1);
    }

    void SelectWeapon(int index)
    {
        selectedWeaponIndex = index;
        katana.SetActive(index == 0);
        glockG22.SetActive(index == 1);
        UpdateWeaponUI();
    }

    void UpdateWeaponUI()
    {
        string[] weaponNames = { "Katana", "Glock G22" };
        weaponUIManager.UpdateWeaponUI(selectedWeaponIndex, weaponNames);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Handle player death (e.g., reload scene, show game over screen)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}