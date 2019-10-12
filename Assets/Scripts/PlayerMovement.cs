using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed = 5f;
    public float runSpeed = 1f;
    public Rigidbody2D rb;

    private Vector3 movement;
    private float movspd;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInputs();
         
    }
    
    void FixedUpdate()
    {
        Move();
    }
    void LateUpdate(){
        
    }

    // Process the inputs of the controller axis
    void ProcessInputs()
    {
        movement.x = Input.GetAxisRaw("Horizontal"); 
        movement.y = Input.GetAxisRaw("Vertical");
        
        // RUN
        if (Input.GetButton("Fire3"))
            runSpeed = 1.5f;
        else
            runSpeed = 1f;

        movspd = Mathf.Clamp(movement.magnitude, 0.0f, 1.0f);
        movement.Normalize();
    }

    // 
    void Move(){
       rb.velocity = movement * movspd * moveSpeed * runSpeed;
    }
}
