using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttack : AttackBase
{
    [SerializeField] private GameObject attackProjectile;

    public new void Attack()
    {
        if (canAttack)
        {
            GameObject spawnedProjectile = Instantiate(attackProjectile, transform.position, transform.rotation);
            spawnedProjectile.GetComponentInChildren<ProjectileBehavior>().SetMoveDirection(transform.root);
            spawnedProjectile.tag = transform.root.tag;

            base.Attack();
        }
    }
}
