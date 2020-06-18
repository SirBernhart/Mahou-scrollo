using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttack : AttackBase
{
    [SerializeField] private GameObject attackProjectile;
    [SerializeField] private float projectileSpeed;

    public new void Attack(ActionType attackType)
    {
        if (canAttack)
        {
        Instantiate(attackProjectile, transform.position, transform.rotation)
                .GetComponentInChildren<ProjectileBehavior>().
                SetProjectileProperties(GetAttackDirection(), currentDamageValue, projectileSpeed, transform.root.gameObject.tag);

            base.Attack(attackType);
        }
    }

    private Vector2 GetAttackDirection()
    {
        if (transform.root.rotation.y == 0)
            return new Vector2(1, 0);
        else
            return new Vector2(-1, 0);
    }
}
