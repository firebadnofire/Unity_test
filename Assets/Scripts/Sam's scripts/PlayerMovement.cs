using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{





    [Header("Movement")]
    public float moveSpeed;
    public float sprintSpeed;

    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    public bool readyToJump; //bool for jumping

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;


    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    public bool grounded;
    public bool isOnGround;


    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        readyToJump = true;
        grounded = true;
        
    }

    private void Update()
    {
        //ground check
        isOnGround = Physics.Raycast(transform.position, new Vector3(0f, -20f, 0), playerHeight * 1.3f + 0.5f, whatIsGround);

        MyInput();
        SpeedControl();

        if (isOnGround == true)
        {
            readyToJump = true;
        }

        //Visualizing Raycast
        Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
        UnityEngine.Debug.DrawRay(transform.position, forward, Color.green);



        //handle drag
        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 5;


    }

   


    private void FixedUpdate()
    {
        MovePlayer();


    }


    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");


        // Takes input from spacebar to cause the player model to jump
        if (Input.GetKeyDown(jumpKey) && readyToJump && isOnGround)
        {
            readyToJump = false;
            Jump();

            ResetJump();
        }
    }

    private void MovePlayer()
    {
        //Calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        

        //Sprint movement
        if (Input.GetKey(sprintKey))
        {
            // on ground sprinting
            if (grounded)
                rb.AddForce(moveDirection.normalized * sprintSpeed * 10f, ForceMode.Force);

            // in air sprinting
            else if (!grounded)
                rb.AddForce(moveDirection.normalized * sprintSpeed * 10f * airMultiplier, ForceMode.Force);
        }
        
        //Walking Movement
        else
        {
            // on ground walking
            if (grounded)
                rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

            // in air walking
            else if (!grounded)
                rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        }

        
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        //limits velocity if greater than base sprintSpeed
        if (Input.GetKeyDown(sprintKey))
        {
            if (flatVel.magnitude > sprintSpeed)
            {
                Vector3 limitedVel = flatVel.normalized * sprintSpeed;
                rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
            }

        }
        //limits velocity if greater than base movespeed
        else if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }

    }

    //Jump variables

    private void Jump()
    {
        // reset y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);

        readyToJump = false;
    }




    //This is supposed to check to see if the player is on the ground to determine whether or not they are allowed to jump again
    private void ResetJump()
    {
        if (isOnGround == true)
        {
            readyToJump = true;
        }
    }





    // Event callback example: Debug-draw all contact points and normals for 2 seconds.
    void OnCollisionEnter(Collision collision)
    {
        UnityEngine.Debug.DrawRay(collision.contacts[0].point, collision.contacts[0].normal, Color.green, 10, false);
    }

}
