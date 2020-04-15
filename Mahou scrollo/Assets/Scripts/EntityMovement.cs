using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityMovement : MonoBehaviour
{
    // Public variables (Balancing tools)
    [SerializeField] private float movespeed;
    [SerializeField] private float jumpVelocity;
    [SerializeField] private float inputMemoryMaxTime;

    // External references
    [SerializeField] private Rigidbody2D entityRigidbody;
    [SerializeField] private GroundedController groundedController;

    // Internal variables
    private Coroutine rememberJumpInputCoroutine;

    private void Start()
    {
        movespeed *= 100;
        jumpVelocity *= 100;
    }

    public void MoveInDirection(float direction)
    {
        direction = direction * movespeed * Time.deltaTime;
        Vector2 moveAmount = new Vector2 (direction, entityRigidbody.velocity.y);
        entityRigidbody.velocity = moveAmount;
    }

    public void Jump()
    {
        if (groundedController.GetIsGrounded())
        {
            if (rememberJumpInputCoroutine != null)
            {
                StopCoroutine(rememberJumpInputCoroutine);
                rememberJumpInputCoroutine = null;
            }

            entityRigidbody.velocity = new Vector2(entityRigidbody.velocity.x, jumpVelocity * Time.deltaTime);
        }
        else
        {
            if (rememberJumpInputCoroutine != null)
            {
                StopCoroutine(rememberJumpInputCoroutine);
            }

            rememberJumpInputCoroutine = StartCoroutine("RememberJumpInputTimer");
        }
    }

    private IEnumerator RememberJumpInputTimer()
    {
        for(float i = 0 ; i < inputMemoryMaxTime ; i += Time.deltaTime)
        {
            Debug.Log("Awainting ground");
            if (groundedController.GetIsGrounded())
            {
                Debug.Log("Grounded!!");
                Jump();
            }
            yield return null;
        }
    }
}
