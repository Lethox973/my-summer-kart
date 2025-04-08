using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float lookSpeedX = 2f;
    public float lookSpeedY = 2f;
    public Transform playerCamera;
    
    public float jumpForce = 5f;
    private float rotationX = 0f;
    private bool isGrounded;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Cursor.lockState = CursorLockMode.Locked;  // This hides the cursor and locks it in the center
        Cursor.visible = false;  // Makes the cursor invisible

        Cursor.lockState = CursorLockMode.Locked;  // This hides the cursor and locks it in the center
        Cursor.visible = false;  // Makes the cursor invisible
        // Mouse Look
        float mouseX = Input.GetAxis("Mouse X") * lookSpeedX;
        float mouseY = Input.GetAxis("Mouse Y") * lookSpeedY;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -80f, 80f); // Limits up/down view
        playerCamera.localRotation = Quaternion.Euler(rotationX, 0f, 0f); // Up/down movement
        transform.Rotate(Vector3.up * mouseX); // Left/right movement

        // Movement
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.1f);

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);  // Jump
        }

        Vector3 moveDirection = transform.right * moveX + transform.forward * moveZ;
        rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.deltaTime);  // Move the player
    }
}
