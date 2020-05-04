using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyBehaviour : MonoBehaviour
{
    [SerializeField] private EntityMovement entityMovement;

    // Update is called once per frame
    void Update()
    {
        entityMovement.MoveInDirection(-1);    
    }
}
