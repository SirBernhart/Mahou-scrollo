﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : AttackBase
{
    [SerializeField] private Collider2D attackCollider;
    [SerializeField] private float attackDuration;
    [SerializeField] private Animator animator;
    
    public GameObject attackIndicator;
    public ParticleSystem launchingKick;

    private void Start()
    {
        launchingKick.Stop();
    }

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
        if (attackIndicator != null)
            attackIndicator.SetActive(true);
        switch (currentAttack.name)
        {
            case "Jab":
                if(attackIndicator != null)
                    attackIndicator.GetComponent<SpriteRenderer>().color = new Color(255, 185, 0);
                if(animator != null)
                    animator.SetTrigger("Punched");
                break;
            case "Cross":
                if (animator != null)
                    animator.SetTrigger("Punched");
                break;
            case "Kick":
                if (animator != null)
                    animator.SetTrigger("Kicked");
                break;
            case "LaunchingKick":
                if (animator != null)
                    animator.SetTrigger("Kicked");
                launchingKick.Play();
                break;
        }

        yield return new WaitForSeconds(attackDuration);

        // End attack
        isAttacking = false;
        attackCollider.enabled = false;
        if (attackIndicator != null)
            attackIndicator.SetActive(false);
        if (launchingKick != null)
            launchingKick.Stop();
    }
}
