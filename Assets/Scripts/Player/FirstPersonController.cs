using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float lookSpeed = 2f;
    public float jumpForce = 2f;
    public float gravity = -9.8f;
    public float maxLookAngle = 80f;  // Maximum up/down rotation

    public float normalFOV = 60f;
    public float zoomedFOV = 40f;
    public float zoomSpeed = 10f;

    [SerializeField] private Transform cameraTransform; // Reference to the camera's transform

    private Camera playerCamera;  // Reference to the Camera component
    private CharacterController characterController;
    private Vector3 velocity;
    private bool isGrounded;

    private float cameraPitch = 0f;  // Vertical camera rotation (pitch)

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        
        // If no camera transform is assigned, try to find the main camera
        if (cameraTransform == null)
        {
            Camera mainCam = Camera.main;
            if (mainCam != null)
            {
                cameraTransform = mainCam.transform;
            }
        }

        // Get the Camera component
        if (cameraTransform != null)
        {
            playerCamera = cameraTransform.GetComponent<Camera>();
            if (playerCamera != null)
            {
                playerCamera.fieldOfView = normalFOV;
            }
        }

        // Lock and hide cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        // Ground check
        isGrounded = characterController.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // Movement input
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        characterController.Move(move * moveSpeed * Time.deltaTime);

        // Jumping
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        }

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;

        // Move the character
        characterController.Move(velocity * Time.deltaTime);

        // Camera rotation
        float mouseX = Input.GetAxis("Mouse X") * lookSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * lookSpeed;

        // Rotate player body horizontally
        transform.Rotate(0, mouseX, 0);

        // Rotate camera vertically and clamp the rotation angle
        cameraPitch -= mouseY; // Note: Changed from += to -= since we removed the negative sign from mouseY
        cameraPitch = Mathf.Clamp(cameraPitch, -maxLookAngle, maxLookAngle);

        if (cameraTransform != null)
        {
            cameraTransform.localEulerAngles = new Vector3(cameraPitch, 0, 0);
        }
        else
        {
            Debug.LogWarning("No camera transform assigned to FirstPersonController!");
        }

        // Zoom in and out when pressing Left Ctrl
        if (playerCamera != null)
        {
            float targetFOV = Input.GetKey(KeyCode.LeftControl) ? zoomedFOV : normalFOV;
            playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, targetFOV, Time.deltaTime * zoomSpeed);
        }
    }
}
