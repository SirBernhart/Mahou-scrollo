using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityAttack : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private Collider2D attackCollider;
    [SerializeField] private float attackCooldown;
    public GameObject attackIndicator;

    private bool canAttack = true;

    public void DoMeleeAttack()
    {
        if (canAttack)
        {
            StartCoroutine(DisableAttackCollider());
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

    IEnumerator DisableAttackCollider()
    {
        attackCollider.enabled = true;
        attackIndicator.SetActive(true);
        yield return new WaitForSeconds(0.2f);
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
