using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityAttack : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private Collider2D attackCollider;
    [SerializeField] private float attackCooldown;
    public GameObject attackIndicator;

    private bool isAttacking;
    public bool GetIsAttacking() { return isAttacking; }

    private bool canAttack = true;
    public void SetCanAttack(bool canAttack)
    {
        this.canAttack = canAttack;

        // If an entity takes damage, it flinches, becoming uncapable of attacking for a short while
        if (!canAttack)
        {
            StopAllCoroutines();
            isAttacking = false;
        }
    }
    public bool GetCanAttack() { return canAttack; }

    public void DoMeleeAttack()
    {
        if (canAttack)
        { 
            StartCoroutine(TemporalilyEnableAttackCollider());
            StartCoroutine(WaitAttackCooldown());
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

        yield return new WaitForSeconds(0.2f);

        // End attack
        isAttacking = false;
        attackCollider.enabled = false;
        attackIndicator.SetActive(false);
    }

    IEnumerator WaitAttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }
}
