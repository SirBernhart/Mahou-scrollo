using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDetector : MonoBehaviour
{
    [SerializeField] private BasicEnemyBehaviour basicEnemyBehaviour;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && collision.name != "Projectile(Clone)")
        {
            basicEnemyBehaviour.ChangeState(BehaviourState.Attacking);
        }
    }
}
