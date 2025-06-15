using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public bool isPlayer1; 
    public float speed = 10f;
    public Rigidbody2D rb;
    private float movement;

    void Update()
    {
        movement = 0f;
        
        if (isPlayer1)
        {
            // Player 1 uses W/S keys
            if (Input.GetKey(KeyCode.W))
                movement = 1f;
            else if (Input.GetKey(KeyCode.S))
                movement = -1f;
        }
        else
        {
            // Player 2 uses Up/Down arrow keys
            if (Input.GetKey(KeyCode.UpArrow))
                movement = 1f;
            else if (Input.GetKey(KeyCode.DownArrow))
                movement = -1f;
        }

        rb.velocity = new Vector2(rb.velocity.x, movement * speed);
    }
}

