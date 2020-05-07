using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    // External references
    [SerializeField] private EntityMovement playerMovement;
    [SerializeField] private TransformationController transformationController;
    [SerializeField] private EntityAttack entityAttack;

    // Internal variables
    private float moveDirectionX;

    // Update is called once per frame
    void Update()
    {
        moveDirectionX = 0f;

        moveDirectionX = Input.GetAxisRaw("Horizontal");

        playerMovement.MoveInDirection(moveDirectionX);

        if (Input.GetButtonDown("Jump"))
        {
            playerMovement.Jump();
        }
        if (Input.GetButtonUp("Jump"))
        {
            playerMovement.CutJumpHeight();
        }

        if (Input.GetButtonDown("Transform"))
        {
            //playerMovement.StopAllMovement();
            transformationController.Transform();
        }

        if (Input.GetButtonDown("Attack"))
        {
            entityAttack.DoMeleeAttack();
        }
    }
}
