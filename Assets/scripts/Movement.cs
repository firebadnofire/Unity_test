using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Movement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 10f;
    public float gravity = 20f;
    public float mouseSensitivity = 2f;
    public Transform cameraTransform;

    private CharacterController characterController;
    private Vector3 moveDirection;
    private float pitchRotation = 0f;  // New variable to keep track of pitch rotation


    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Mouse look
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
        transform.Rotate(Vector3.up * mouseX);
        cameraTransform.Rotate(Vector3.right * -mouseY);

        // Updated rotation clamping
        pitchRotation -= mouseY;
        pitchRotation = Mathf.Clamp(pitchRotation, -90f, 90f);
        cameraTransform.localRotation = Quaternion.Euler(pitchRotation, 0, 0);

        // Movement
        float inputX = Input.GetAxis("Horizontal");
        float inputZ = Input.GetAxis("Vertical");
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        Vector3 targetDirection = inputX * right + inputZ * forward;
        targetDirection.Normalize();

        // Jumping
        if (characterController.isGrounded)
        {
            moveDirection.y = 0f;  // Reset vertical speed if on the ground
            if (Input.GetButtonDown("Jump"))
            {
                moveDirection.y = jumpForce;
            }
        }

        // Apply gravity
        moveDirection.y -= gravity * Time.deltaTime;

        // Retain full movement control in air
        Vector3 horizontalMoveDirection = targetDirection * speed;
        moveDirection.x = horizontalMoveDirection.x;
        moveDirection.z = horizontalMoveDirection.z;

        // Move the character
        characterController.Move(moveDirection * Time.deltaTime);
    }
}
