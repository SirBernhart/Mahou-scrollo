using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    // External references
    [SerializeField] private EntityMovement playerMovement;

    // Internal variables
    private Vector2 moveDirection;

    // Update is called once per frame
    void Update()
    {
        moveDirection = Vector2.zero;

        // Teclado
        moveDirection.x = Input.GetAxisRaw("Horizontal");
        moveDirection.y = Input.GetAxisRaw("Vertical");

        playerMovement.MoveInDirection(Vector2.ClampMagnitude(moveDirection, 1f));
    }
}
