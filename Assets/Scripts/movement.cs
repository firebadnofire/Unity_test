using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    private PlayerControls controls;
    private InputAction jumpAction;
    private InputAction moveAction;
    private InputAction cameraAction;

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 10f;

    private CharacterController characterController;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private Transform playerTransform;

    private Transform mainCameraTransform;
    private Vector3 cameraOffset;
    private float cameraRotationSpeed = 2f;

    private void Awake()
    {
        controls = new PlayerControls();
        jumpAction = controls.Jump.Jump;
        moveAction = controls.Walk.Walking;
        cameraAction = controls.Camera.CamMove;
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        playerTransform = transform;

        // Get the main camera's transform and calculate the initial camera offset
        mainCameraTransform = Camera.main.transform;
        cameraOffset = mainCameraTransform.position - playerTransform.position;
    }

    private void Update()
    {
        // Jump
        if (groundedPlayer && jumpAction.triggered)
        {
            playerVelocity.y += Mathf.Sqrt(jumpForce * -3.0f * Physics.gravity.y);
        }

        // Walk
        Vector2 moveInput = moveAction.ReadValue<Vector2>();
        float moveX = moveInput.x;
        float moveY = moveInput.y;

        Vector3 moveDirection = new Vector3(moveX, 0f, moveY).normalized;

        if (moveDirection != Vector3.zero)
        {
            Vector3 moveDir = playerTransform.forward * moveDirection.z + playerTransform.right * moveDirection.x;
            characterController.Move(moveDir * moveSpeed * Time.deltaTime);
        }

        // Apply gravity
        groundedPlayer = characterController.isGrounded;
        if (!groundedPlayer)
        {
            playerVelocity.y += Physics.gravity.y * Time.deltaTime;
        }
        else
        {
            playerVelocity.y = 0f;
        }

        characterController.Move(playerVelocity * Time.deltaTime);

        // Orbit the camera around the player
        Vector2 cameraInput = cameraAction.ReadValue<Vector2>();
        float cameraX = cameraInput.x * cameraRotationSpeed;

        // Rotate the camera around the player's position
        mainCameraTransform.RotateAround(playerTransform.position, Vector3.up, cameraX);
    }
}
