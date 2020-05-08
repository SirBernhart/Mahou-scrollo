using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BehaviourState {Idle, Patrol, Chasing, Attacking, Dying }

public class BasicEnemyBehaviour : MonoBehaviour
{
    [SerializeField] private EntityMovement entityMovement;
    [SerializeField] private PlayerChaseDetector playerChaseDetector;
    [SerializeField] private AttackDetector attackDetector;
    [SerializeField] private EntityAttack attackController;
    private bool canChangeState = true;

    private GameObject playerReference;
    public GameObject GetPlayerReference() { return playerReference; }
    public void SetPlayerReference(GameObject playerReference) { this.playerReference = playerReference; }

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
            case BehaviourState.Dying:
                Dying();
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
        attackDetector.gameObject.SetActive(true);
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
        
    }

    private void Attacking()
    {
        if(attacking == null)
        {
            attacking = StartCoroutine(DoAttack());
        }
    }

    private void Dying()
    {
        canChangeState = false;
        StopCoroutine(attacking);
        playerChaseDetector.gameObject.SetActive(false);
        attackDetector.gameObject.SetActive(false);
    }

    private IEnumerator DoAttack()
    {
        playerChaseDetector.gameObject.SetActive(false);
        attackDetector.gameObject.SetActive(false);
        canChangeState = false;

        yield return new WaitForSeconds(0.8f);
        attackController.DoMeleeAttack();
        yield return new WaitForSeconds(0.3f);
        canChangeState = true;
        playerChaseDetector.gameObject.SetActive(true);
        ChangeState(BehaviourState.Idle);
        attacking = null;
    }
}
