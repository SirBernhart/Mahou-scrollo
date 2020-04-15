using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityMovement : MonoBehaviour
{
    // Public variables (Balancing tools)
    [SerializeField] private float movespeed;
    [SerializeField] private float jumpVelocity;

    // External references
    [SerializeField] private Rigidbody2D entityRigidbody;

    // Internal variables
    private bool isGrounded = true;

    private void Start()
    {
        movespeed *= 100;
        jumpVelocity *= 100;
    }

    private void Update()
    {
        //if()
    }


    public void MoveInDirection(float direction)
    {
        direction = direction * movespeed * Time.deltaTime;
        Vector2 moveAmount = new Vector2 (direction, entityRigidbody.velocity.y);
        entityRigidbody.velocity = moveAmount;
    }

    public void Jump()
    {
        if (isGrounded)
        {
            entityRigidbody.velocity = new Vector2(entityRigidbody.velocity.x, jumpVelocity * Time.deltaTime);
        }
    }
}
