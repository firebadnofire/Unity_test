using UnityEngine;

public class Movement : MonoBehaviour
{
    private PlayerControls playerControls;
    private Rigidbody playerRigidbody;
    public float moveSpeed = 5.0f;
    public float jumpForce = 10.0f;
    public Transform cameraTransform;
    public float cameraFollowSpeed = 5.0f;

    private void Awake()
    {
        playerControls = new PlayerControls();
        playerRigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void Update()
    {
        // Get the input values
        Vector2 moveInput = playerControls.Player.Move.ReadValue<Vector2>();
        bool jumpInput = playerControls.Player.Jump.triggered;

        // Calculate movement direction based on input
        Vector3 movementDirection = new Vector3(moveInput.x, 0.0f, moveInput.y);
        movementDirection = cameraTransform.TransformDirection(movementDirection);
        movementDirection.y = 0.0f; // Ensure the player doesn't move up/down.

        // Move the player
        playerRigidbody.velocity = movementDirection.normalized * moveSpeed;

        // Make the player jump
        if (jumpInput)
        {
            playerRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        // Camera follow the player
        Vector3 targetPosition = transform.position;
        targetPosition.y = cameraTransform.position.y; // Keep the camera's Y position unchanged
        cameraTransform.position = Vector3.Lerp(cameraTransform.position, targetPosition, cameraFollowSpeed * Time.deltaTime);
    }
}
