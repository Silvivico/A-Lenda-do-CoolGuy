using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Player_Move : NetworkBehaviour
{
    public float moveSpeed = 5f; 
    public Rigidbody2D rb; 

    Vector2 movement; 
    Vector2 mousePos; 

    void Update()
    {
        if (!IsOwner) return;
        
        movement = Vector2.zero;

        
        if (Input.GetKey(KeyCode.W)) movement.y = 1;   
        if (Input.GetKey(KeyCode.S)) movement.y = -1; 
        if (Input.GetKey(KeyCode.A)) movement.x = -1; 
        if (Input.GetKey(KeyCode.D)) movement.x = 1; 

        
        movement = movement.normalized;

        
    }

    void FixedUpdate()
    {
        
        rb.velocity = movement * moveSpeed;

    }
}
