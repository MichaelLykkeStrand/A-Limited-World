using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    private Rigidbody2D rb;
    public bool canMove = true;

    Vector2 movement;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (canMove)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
        }
        else { movement = Vector2.zero; }
        
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + (moveSpeed * Time.fixedDeltaTime * movement.normalized));
    }

}
