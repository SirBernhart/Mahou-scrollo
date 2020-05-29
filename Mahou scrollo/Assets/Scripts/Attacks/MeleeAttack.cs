using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : AttackBase
{
    [SerializeField] private Collider2D attackCollider;
    [SerializeField] private float attackDuration;
    
    public GameObject attackIndicator;

    public new void Attack(ActionType attackType)
    {
        if (canAttack && health.GetCanAct())
        {
            base.Attack(attackType);
            StartCoroutine(TemporalilyEnableAttackCollider());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((transform.parent.gameObject.tag == "Player" && collision.tag == "Enemy") ||
            (transform.parent.gameObject.tag == "Enemy" && collision.tag == "Player"))
        {
            effectsHandler.HandlSpecialAttackEffect(currentAttack, collision.gameObject);
            collision.GetComponentInChildren<Health>().ReduceHealth(currentDamageValue);
        }
    }

    IEnumerator TemporalilyEnableAttackCollider()
    {
        // Start attack
        isAttacking = true;
        attackCollider.enabled = true;
        attackIndicator.SetActive(true);
        switch (currentAttack.name)
        {
            case "Jab":
                attackIndicator.GetComponent<SpriteRenderer>().color = new Color(255, 185, 0);
                break;
            case "Cross":
                attackIndicator.GetComponent<SpriteRenderer>().color = new Color(255, 0, 0);
                break;
            case "Kick":
                attackIndicator.GetComponent<SpriteRenderer>().color = new Color(0, 255, 0);
                break;
            case "LaunchingKick":
                attackIndicator.GetComponent<SpriteRenderer>().color = new Color(255, 0, 125);
                break;
        }

        yield return new WaitForSeconds(attackDuration);

        // End attack
        isAttacking = false;
        attackCollider.enabled = false;
        attackIndicator.SetActive(false);
    }
}
