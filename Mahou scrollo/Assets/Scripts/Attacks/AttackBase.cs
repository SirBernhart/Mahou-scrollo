using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBase : MonoBehaviour
{
    [SerializeField] protected int lightDamage;
    [SerializeField] protected int heavyDamage;
    [SerializeField] protected float cooldown;
    [SerializeField] protected Health health;
    [SerializeField] protected ComboManager comboManager;
    [SerializeField] protected SpecialAttackEffectsHandler effectsHandler;

    protected ComboElement currentAttack;
    protected int currentDamageValue;

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

    public void Attack(ActionType attackType)
    {
        if (health.GetCanAct())
        {
            currentAttack = comboManager.AddToCombo(attackType);

            currentDamageValue = currentAttack.damage;

            StartCoroutine(CountAttackCooldown(currentAttack.cooldown));
        }
    }

    private IEnumerator CountAttackCooldown(float cooldown)
    {
        canAttack = false;
        yield return new WaitForSeconds(cooldown);
        canAttack = true;
    }
}
