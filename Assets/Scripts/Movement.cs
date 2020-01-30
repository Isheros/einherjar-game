using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Velocidades al correr y caminar
    public float moveSpeed = 5f;
    public float runSpeed = 1f;
    private Vector3 movement = Vector3.zero;

    // Fuerza de salto y switchs para saltar
    public float jumpForce = 1f;
    public bool grnd;
    public bool jumping;

    // Pra el tiempo de salto
    public float jumpTime;
    public float jumpTimeCount;

    // Distancia del raycast
    public float rayDistance = 1f;

    //Rigidbody
    public Rigidbody rb;
    public GameObject cameraObject;
    
    // Cambio de personaje
    public int charNumber = 0;
    public bool activateMove;

    // Debug
    public Vector3 vel;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {   
        activateMove = cameraObject.GetComponent<CameraFollow>().characterSwitch;
        if ((activateMove ? 1 : 0) == charNumber){
            grnd = IsGrounded();

            ProcessMovement();
            Jump();
        }

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

    // Mueve al personaje
    private void Move()
    {
        rb.AddForce(movement * moveSpeed * runSpeed, ForceMode.VelocityChange);
    }

    // 
    private void Jump()
    {
        // Si esta tocando el suelo y se aprieta el boton saltar, saltara
        if (IsGrounded() && Input.GetButtonDown("Jump"))
        {
            jumping = true;
            jumpTimeCount = jumpTime;
            rb.AddForce(0, jumpForce , 0, ForceMode.Impulse);
        }

        // Modula el tiempo de salto mientras se deja presionado el boton
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
        
        // Si se suelta el boton, se saldra del estado saltar
        if (Input.GetButtonUp("Jump"))
        {
            jumping = false;
        }
    }

    // Revisa mediante un raycast, si esta tocando el suelo
    private bool IsGrounded()
    {
        Debug.DrawRay(transform.position, Vector3.down * rayDistance, Color.blue);
        return Physics.Raycast(transform.position, Vector3.down, rayDistance);
    }
}
