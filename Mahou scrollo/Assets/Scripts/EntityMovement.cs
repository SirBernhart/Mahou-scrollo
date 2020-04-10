using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityMovement : MonoBehaviour
{
    // Public variables (Balancing tools)
    [SerializeField] private float movespeed;

    // External references
    [SerializeField] private Rigidbody2D entityRigidbody;

    private void Start()
    {
        movespeed *= 100;
    }

    public void MoveInDirection(Vector2 direction)
    {
        Vector3 moveAmount = direction * movespeed * Time.deltaTime;
        entityRigidbody.velocity = moveAmount; 
    }
}
