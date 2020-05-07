using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BehaviourState {Idle, Patrol, Chasing, Attacking}

public class BasicEnemyBehaviour : MonoBehaviour
{
    [SerializeField] private EntityMovement entityMovement;
    [SerializeField] private ObstacleDetector obstacleDetector;
    [SerializeField] private EntityAttack attackController;
    private bool canChangeState = true;

    public GameObject playerReference;

    private Coroutine attacking;

    private BehaviourState currentState = BehaviourState.Idle;

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case BehaviourState.Chasing:
                Chase();
                break;

            case BehaviourState.Idle:
                Idle();
                break;

            case BehaviourState.Attacking:
                Attacking();
                break;

            case BehaviourState.Patrol:
                //Patrol();
                break;
        }
    }

    public void ChangeState(BehaviourState nextBehaviour)
    {
        if (canChangeState)
        {
            currentState = nextBehaviour;
        }
    }

    private void Chase()
    {
        // move in direction of player
        if(playerReference.transform.position.x >= transform.position.x)
        {
            entityMovement.MoveInDirection(1);
        }
        else
        {
            entityMovement.MoveInDirection(-1);
        }
    }

    private void Idle()
    {
        // do nothing
    }

    private void Attacking()
    {
        if(attacking == null)
        {
            entityMovement.StopWalking();

            attacking = StartCoroutine(DoAttack());
        }
    }

    private IEnumerator DoAttack()
    {
        canChangeState = false;

        yield return new WaitForSeconds(1);
        attackController.DoMeleeAttack();

        canChangeState = true;
        ChangeState(BehaviourState.Idle);
        attacking = null;
    }
}
