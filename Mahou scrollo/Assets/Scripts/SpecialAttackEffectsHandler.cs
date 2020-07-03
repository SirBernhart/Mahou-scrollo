using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAttackEffectsHandler : MonoBehaviour
{
    public void HandlSpecialAttackEffect(ComboElement currentAttack, GameObject target)
    {
        switch (currentAttack.specialEffect)
        {
            case SpecialEffect.knockback:
                ApplyKnockback(currentAttack, target);
                break;
            case SpecialEffect.stun:
                //stun
                break;
        }
    }

    private void ApplyKnockback(ComboElement currentAttack, GameObject target)
    {
        float xDirection;

        if (transform.root.position.x < target.transform.position.x)
            xDirection = 1;
        else
            xDirection = -1;
        
        //xDirection *= 3;
        target.GetComponentInChildren<EntityMovement>().StopAllMovement();
        target.GetComponent<Rigidbody2D>().velocity = new Vector2(xDirection, 0.5f) * currentAttack.specialEffectIntensity;
    }
}
