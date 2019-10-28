using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public float moveSpeed = 5f;
    public float runSpeed = 1f;

    public float jumpForce = 1f;
    public float rayDistance = 1f;

    public Rigidbody rb;
    public bool grnd;
    public bool jumping;

    public Vector3 vel;

    private Vector3 movement = Vector3.zero;

    public float jumpTime;
    public float jumpTimeCount;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        grnd = IsGrounded();

        ProcessMovement();
        Jump();
    }

    void FixedUpdate()
    {
        vel = rb.velocity;
        Move();
        
    }
    void LateUpdate()
    {

    }

    // Process the inputs of the controller axis
    private void ProcessMovement()
    {
        // Movement
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = 0f;
        movement.z = Input.GetAxisRaw("Vertical");

        // Run
        if (Input.GetButton("Fire3"))
            runSpeed = 1.5f;
        else
            runSpeed = 1f;

        movement.Normalize();
    }
    // Move the character
    private void Move()
    {
        rb.AddForce(movement * moveSpeed * runSpeed, ForceMode.VelocityChange);
    }

    // 
    private void Jump()
    {

        if (IsGrounded() && Input.GetButtonDown("Jump"))
        {
            jumping = true;
            jumpTimeCount = jumpTime;
            rb.AddForce(0, jumpForce , 0, ForceMode.Impulse);
        }
        if (Input.GetButton("Jump") && jumping)
        {
            if (jumpTimeCount > 0)
            {
                rb.AddForce(0, jumpForce, 0, ForceMode.Impulse);
                jumpTimeCount -= Time.deltaTime;
            }
            else
            {
                jumping = false;
            }
        }

        if (Input.GetButtonUp("Jump"))
        {
            jumping = false;
        }
    }
    private bool IsGrounded()
    {
        Debug.DrawRay(transform.position, Vector3.down * rayDistance, Color.blue);
        return Physics.Raycast(transform.position, Vector3.down, rayDistance);
    }
}
