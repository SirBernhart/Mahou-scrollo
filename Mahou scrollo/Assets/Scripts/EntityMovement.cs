using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityMovement : MonoBehaviour
{
    // Public variables (Balancing tools)
    [SerializeField] private float maxMovespeed;
    [SerializeField] private float acceleration;
    [SerializeField] private float deacceleration;
    [SerializeField] private float jumpVelocity;
    [SerializeField] private float cutJumpHeightAmount;
    [SerializeField] private float gravityScale;
    [SerializeField] private float inputMemoryMaxTime;

    // External references
    [SerializeField] private Rigidbody2D entityRigidbody;
    [SerializeField] private GroundedController groundedController;
    [SerializeField] private AttackBase entityAttack;

    // Internal variables
    private Coroutine rememberJumpInputCoroutine;
    private float currentMoveSpeed;

    private void Start()
    {
        acceleration *= 10;
        deacceleration *= 10;
    }

    public void MoveInDirection(float direction)
    {
        currentMoveSpeed = entityRigidbody.velocity.x;
        float deaccelerationDirection = (-1)*(currentMoveSpeed / Mathf.Abs(currentMoveSpeed));

        if (!entityAttack.GetIsAttacking())
        {
            // Sets the direction the entity is facing
            if(direction > 0)
            {
                transform.root.transform.eulerAngles = new Vector3(0, 0, 0);
            }
            else if(direction < 0)
            {
                transform.root.transform.eulerAngles = new Vector3(0, 180, 0);
            }

            // Applies the correct acceleration, depending on the situation
            if (direction != 0 && Mathf.Abs(currentMoveSpeed) <= maxMovespeed)
            {
                if(direction == deaccelerationDirection)
                {
                    Deaccelerate(deaccelerationDirection);
                }
                else
                {
                    Accelerate(direction);
                }
            
            }
            // The entity stopped inputting movement but hasn't stopped moving yet
            else if(direction == 0 && Mathf.Abs(currentMoveSpeed) > 0)
            {
                Deaccelerate(deaccelerationDirection);
            }

            float modifiedGravity = Physics2D.gravity.y;
            if(entityRigidbody.velocity.y < 0)
            {
                modifiedGravity *= (2.5f * Time.deltaTime);
            }
            else
            {
                modifiedGravity *= (1.5f * Time.deltaTime);
            }

            Vector2 moveAmount = new Vector2 (currentMoveSpeed, entityRigidbody.velocity.y + modifiedGravity);
            entityRigidbody.velocity = moveAmount;
        }
        else
        {
            StopAllMovement();
        }
    }

    private void Accelerate(float direction)
    {
        currentMoveSpeed += direction * acceleration * Time.deltaTime;
        if (Mathf.Abs(currentMoveSpeed) >= maxMovespeed)
        {
            currentMoveSpeed = maxMovespeed * direction;
        }
    }

    private void Deaccelerate(float direction)
    {
        currentMoveSpeed += direction * deacceleration * Time.deltaTime;
        if ((direction == -1 && currentMoveSpeed < 0)
            || (direction == 1 && currentMoveSpeed > 0))
        {
            currentMoveSpeed = 0;
        }
    }

    public void Jump()
    {
        if (!entityAttack.GetIsAttacking())
        {
            if (groundedController.GetIsGrounded())
            {
                if (rememberJumpInputCoroutine != null)
                {
                    StopCoroutine(rememberJumpInputCoroutine);
                    rememberJumpInputCoroutine = null;
                }

                entityRigidbody.velocity = new Vector2(entityRigidbody.velocity.x, jumpVelocity);
                groundedController.ReactToPlayerJump();
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
    }

    public void CutJumpHeight()
    {
        if(entityRigidbody.velocity.y > 0)
        {
            entityRigidbody.velocity = new Vector2(entityRigidbody.velocity.x, entityRigidbody.velocity.y * cutJumpHeightAmount);
        }
    }

    public void StopWalking()
    {
        entityRigidbody.velocity = new Vector2 (0, entityRigidbody.velocity.y);
    }

    public void StopAllMovement()
    {
        entityRigidbody.velocity = Vector2.zero;
    }

    private IEnumerator RememberJumpInputTimer()
    {
        for(float i = 0 ; i < inputMemoryMaxTime ; i += Time.deltaTime)
        {
            if (groundedController.GetIsGrounded())
            {
                Jump();
            }
            yield return null;
        }
    }
}
