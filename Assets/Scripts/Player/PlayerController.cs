using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float checkDistance = 0.6f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void FixedUpdate()
    {
        // 1. Input
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputZ = Input.GetAxisRaw("Vertical");
        Vector3 inputDir = new Vector3(inputX, 0f, inputZ).normalized;

        // 2. World direction
        Vector3 moveDir = transform.TransformDirection(inputDir);

        // 3. Wall check per direction (slide fix)
        Vector3 adjustedMoveDir = moveDir;
        if (inputDir != Vector3.zero)
        {
            // Cast a sphere to detect walls
            if (Physics.SphereCast(transform.position, 0.3f, moveDir, out RaycastHit hit, checkDistance))
            {
                // Slide along the wall
                adjustedMoveDir = Vector3.ProjectOnPlane(moveDir, hit.normal).normalized;
            }
        }

        // 4. Apply velocity
        Vector3 finalVelocity = adjustedMoveDir * moveSpeed;
        rb.linearVelocity = new Vector3(finalVelocity.x, rb.linearVelocity.y, finalVelocity.z);
    }
}
