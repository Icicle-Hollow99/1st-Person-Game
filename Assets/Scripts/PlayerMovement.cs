using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float sprintSpeed = 8f;
    public float jumpForce = 10f;
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

        weaponUIManager = FindObjectOfType<WeaponUIManager>();
        currentHealth = maxHealth;

        // Initialize the weapon UI
        UpdateWeaponUI();

        // Lock the cursor and ensure the cursor is visible
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
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
            Cursor.SetCursor(dotCursor, Vector2.zero, CursorMode.Auto); // Ensure cursor is visible
        }
        else if (Input.GetMouseButtonDown(0)) // Lock the cursor when the left mouse button is clicked
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = true; // Ensure the cursor is visible
            Cursor.SetCursor(dotCursor, Vector2.zero, CursorMode.Auto); // Ensure cursor is visible
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
            rb.linearVelocity = new Vector3(movement.x * sprintSpeed, rb.linearVelocity.y, movement.z * sprintSpeed);
        }
        else
        {
            rb.linearVelocity = new Vector3(movement.x * speed, rb.linearVelocity.y, movement.z * speed);
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
            isGrounded = false; // Prevents double jumping
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

    void FixedUpdate()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.1f);
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
