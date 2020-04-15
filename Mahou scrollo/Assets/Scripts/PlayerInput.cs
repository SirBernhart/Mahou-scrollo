using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    // External references
    [SerializeField] private EntityMovement playerMovement;

    // Internal variables
    private float moveDirectionX;

    // Update is called once per frame
    void Update()
    {
        moveDirectionX = 0f;

        moveDirectionX = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            playerMovement.Jump();
        }

        playerMovement.MoveInDirection(moveDirectionX);
    }
}
