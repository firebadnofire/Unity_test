using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 5.0f;
    public float rotationSpeed = 120.0f;
    public float jumpForce = 10.0f;
    public float gravity = 20.0f;

    private CharacterController characterController;
    private Vector3 moveDirection = Vector3.zero;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Get the input axes
        float moveForward = Input.GetAxis("Vertical");
        float moveSideways = Input.GetAxis("Horizontal");

        // Calculate movement direction based on input
        Vector3 move = transform.forward * moveForward + transform.right * moveSideways;
        move.Normalize();

        // Move the character controller
        moveDirection = move * speed;

        // Apply gravity
        if (!characterController.isGrounded)
            moveDirection.y -= gravity * Time.deltaTime;

        // Jump
        if (characterController.isGrounded && Input.GetButtonDown("Jump"))
            moveDirection.y = jumpForce;

        characterController.Move(moveDirection * Time.deltaTime);

        // Rotate the player based on horizontal input
        Vector3 rotation = new Vector3(0, Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime, 0);
        transform.Rotate(rotation);

        // Rotate the camera based on vertical mouse input
        float verticalRotation = -Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;
        Camera.main.transform.Rotate(verticalRotation, 0, 0);
    }
}
