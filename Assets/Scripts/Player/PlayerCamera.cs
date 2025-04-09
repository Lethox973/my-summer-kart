using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private Transform cameraHolder;
    [SerializeField] private float sensitivity = 2f;
    [SerializeField] private float maxVerticalAngle = 80f;

    private float verticalLook = 0f;
    private PlayerInput input;

    private void Awake()
    {
        input = GetComponent<PlayerInput>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        Vector2 look = input.LookInput * sensitivity;

        // Vertical look (cameraHolder)
        verticalLook -= look.y;
        verticalLook = Mathf.Clamp(verticalLook, -maxVerticalAngle, maxVerticalAngle);
        cameraHolder.localRotation = Quaternion.Euler(verticalLook, 0, 0);

        // Horizontal look (player body)
        transform.Rotate(Vector3.up * look.x);
    }
}
