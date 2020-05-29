using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBase : MonoBehaviour
{
    [SerializeField] protected int damage;
    [SerializeField] protected float cooldown;
    [SerializeField] protected Health health;

    protected bool isAttacking;
    public bool GetIsAttacking() { return isAttacking; }

    protected bool canAttack = true;
    public void SetCanAttack(bool canAttack)
    {
        this.canAttack = canAttack;

        if (!canAttack)
        {
            StopAllCoroutines();
            isAttacking = false;
        }
    }
    public bool GetCanAttack() { return canAttack; }

    public void Attack()
    {
        if(health.GetCanAct())
            StartCoroutine(CountAttackCooldown());
    }

    private IEnumerator CountAttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(cooldown);
        canAttack = true;
    }
}
