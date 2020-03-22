using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    
    [Header("Movimiento")]
    // Velocidades al correr y caminar
    public float moveSpeed = 5f;
    public float runSpeed = 1f;
    private Vector3 direction = Vector3.zero;


    [Header("Salto")]
    // Fuerza de salto y switchs para saltar
    public float jumpForce = 1f;
    public bool isJumping;

    // Para el tiempo de salto
    public float jumpTime;
    public float jumpTimeCount;
    private float jump = 0f;
    private Vector3 jumpVel = Vector3.zero;


    [Header("Colisiones")]
    // Distancia del raycast
    public float rayDistance = 1f;

    //Rigidbody
    public Rigidbody rb;
    public GameObject cameraObject;
    
    [Header("Cambio Personaje")]
    // Cambio de personaje
    public int charNumber = 0;
    public bool activateMove;

    // Para seguir al personaje
    public Transform otherPlayer;
    public float distance;
    private float angle;

    [Header("Debug")]
    public bool isGrnd;

    /// ------------------------------------------------------------------------
    /// <summary> 
    /// Start is called before the first frame update 
    /// </summary>
    /// ------------------------------------------------------------------------
    void Start() {}

    /// ------------------------------------------------------------------------
    /// <summary> 
    /// Update is called once per frame 
    /// </summary>
    /// ------------------------------------------------------------------------
    void Update()
    {   
        // Para saber si esta tocando el piso
        isGrnd = IsGrounded();

        activateMove = cameraObject.GetComponent<CameraFollow>().characterSwitch;

        if ((activateMove ? 1 : 0) == charNumber){
            ProcessMovement();
        }else{
            Vector3 p2Direction = otherPlayer.position - transform.position;
            angle = Mathf.Atan2(p2Direction.x, p2Direction.z) * Mathf.Rad2Deg;
            if (p2Direction.magnitude >= distance){
                direction = p2Direction.normalized;
            } else{
                direction = Vector3.zero;
            }
            
        }
    }

    /// ------------------------------------------------------------------------
    /// <summary> 
    /// FixedUpdate is called Frame-rate independent 
    /// </summary>
    /// ------------------------------------------------------------------------
    void FixedUpdate()
    {
        Move();
    }

    /// ------------------------------------------------------------------------
    /// <summary> 
    /// Called every frame, after update 
    /// </summary>
    /// ------------------------------------------------------------------------
    void LateUpdate(){}

    /// ------------------------------------------------------------------------
    /// <summary> 
    /// Procesa la entrada de las palancas 
    /// </summary>
    /// ------------------------------------------------------------------------
    private void ProcessMovement()
    {
        // Obtiene la entrada de las palancas
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.z = Input.GetAxisRaw("Vertical");

        // Obtiene la entrada para correr
        if (Input.GetButton("Fire3"))
            runSpeed = 1.2f;
        else
            runSpeed = 1f;



        direction.Normalize();
    }

    /// ------------------------------------------------------------------------
    /// <summary> 
    /// Aplica todas las fuerzas y transformaciones al personaje 
    /// </summary>
    /// ------------------------------------------------------------------------
    private void Move()
    {
        rb.AddForce(direction * moveSpeed * runSpeed, ForceMode.VelocityChange);
        transform.rotation = Quaternion.AngleAxis(angle - 90f, Vector3.up);
    }

    /// <summary>
    /// Procesa el salto del personaje 
    /// </summary>
    private void Jump()
    {
        // Si esta tocando el suelo y se aprieta el boton saltar, saltara
        if (IsGrounded() && Input.GetButtonDown("Jump"))
        {
            isJumping = true;
            jumpTimeCount = jumpTime;
            rb.AddForce(0, jumpForce , 0, ForceMode.Impulse);
        }

        // Modula el tiempo de salto mientras se deja presionado el boton
        if (Input.GetButton("Jump") && isJumping)
        {
            if (jumpTimeCount > 0)
            {
                rb.AddForce(0, jumpForce, 0, ForceMode.Impulse);
                jumpTimeCount -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }
        
        // Si se suelta el boton, se saldra del estado saltar
        if (Input.GetButtonUp("Jump"))
        {
            isJumping = false;
        }
    }

    /// ------------------------------------------------------------------------
    /// <summary> 
    /// Revisa mediante un raycast, si esta tocando el suelo 
    /// </summary>
    /// ------------------------------------------------------------------------
    private bool IsGrounded()
    {
        Debug.DrawRay(transform.position, Vector3.down * rayDistance, Color.blue);
        return Physics.Raycast(transform.position, Vector3.down, rayDistance);
    }
}
