using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed = 5f;
    public float movspd;
    public Rigidbody2D rb;
    public Vector2 movement;
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

    void ProcessInputs()
    {
        movement = new Vector2(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));
        movspd = Mathf.Clamp(movement.magnitude, 0.0f, 1.0f);
        movement.Normalize();
    }

    void Move(){
        rb.velocity = movement * movspd * moveSpeed;
    }
}
