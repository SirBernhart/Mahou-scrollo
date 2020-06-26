using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    [SerializeField] private Rigidbody2D projectileRigidbody;
    [SerializeField] private ParticleSystem explosion;
    private float speed;
    public void SetSpeed(float speed) { this.speed = speed; }

    private void Start()
    {
        explosion.Stop();
    }

    private int damageAmount;
    public void SetDamageAmount(int damange)
    {
        // Can only be set to a value != 0 once
        if(damageAmount == 0)
        {
            damageAmount = damange;
        }
    }

    public void SetProjectileProperties(Vector2 direction, int damage, float speed, string tag)
    {
        damageAmount = damage;
        this.speed = speed;
        SetMoveDirection(direction);
        gameObject.tag = tag;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Avoids shooter's projectiles colliding with himself
        if (this.gameObject.tag != collision.tag)
        {
            if(collision.tag == "Enemy" || collision.tag == "Player")
            {
                DoDamage(collision.gameObject);
            }
            if (collision.tag != "Untagged")
            {
                explosion.Play();
                DestroyProjectile();
            }
        }
    }

    private void DestroyProjectile()
    {
        GetComponentInChildren<ParticleSystem>().Stop();
        Destroy(transform.root.gameObject);
        Destroy(transform.GetChild(1).gameObject);
        Destroy(transform.GetChild(0).gameObject, 1f);
        transform.DetachChildren();
    }

    public void SetMoveDirection(Vector2 direction)
    {
        projectileRigidbody.velocity = direction * speed;
    }

    private void DoDamage(GameObject entityToDamage)
    {
        Health entityHealthReference = entityToDamage.GetComponentInChildren<Health>();

        // Only damages entities that have the "Health" component
        if (entityHealthReference != null)
        {
            entityHealthReference.ReduceHealth(damageAmount);
        }
    }
}
