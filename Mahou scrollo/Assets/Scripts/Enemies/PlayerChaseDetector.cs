using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChaseDetector : MonoBehaviour
{
    [SerializeField] private BasicEnemyBehaviour basicEnemyBehaviour;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if (basicEnemyBehaviour.playerReference == null)
            {
                basicEnemyBehaviour.playerReference = collision.gameObject;
            }

            basicEnemyBehaviour.ChangeState(BehaviourState.Chasing);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            basicEnemyBehaviour.ChangeState(BehaviourState.Idle);
        }
    }
}
