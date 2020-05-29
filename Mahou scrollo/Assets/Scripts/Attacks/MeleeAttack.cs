using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : AttackBase
{
    [SerializeField] private Collider2D attackCollider;
    [SerializeField] private float attackDuration;
    
    public GameObject attackIndicator;

    public new void Attack()
    {
        if (canAttack && health.GetCanAct())
        { 
            StartCoroutine(TemporalilyEnableAttackCollider());
            base.Attack();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((transform.parent.gameObject.tag == "Player" && collision.tag == "Enemy") ||
            (transform.parent.gameObject.tag == "Enemy" && collision.tag == "Player"))
        {
            collision.GetComponentInChildren<Health>().ReduceHealth(damage);
        }
    }

    IEnumerator TemporalilyEnableAttackCollider()
    {
        // Start attack
        isAttacking = true;
        attackCollider.enabled = true;
        attackIndicator.SetActive(true);

        yield return new WaitForSeconds(attackDuration);

        // End attack
        isAttacking = false;
        attackCollider.enabled = false;
        attackIndicator.SetActive(false);
    }
}
