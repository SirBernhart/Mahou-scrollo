using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float amount;
    [SerializeField] private ParticleSystem deathParticles;
    [SerializeField] private GameObject graphics;
    [SerializeField] private float blinkTime;
    [SerializeField] private EntityAttack entityAttack;

    [SerializeField] private Image healthBar;

    public void ReduceHealth(int amount)
    {
        this.amount -= amount;
        UpdateHealthBarGraphics();

        StartCoroutine(Blink());

        if(this.amount <= 0)
        {
            StartCoroutine(KillEntity());
        } 
    }

    public void IncreaseHealth(int amount)
    {
        this.amount += amount;
        UpdateHealthBarGraphics();
    }

    public void UpdateHealthBarGraphics()
    {
        if (healthBar != null)
        {
            healthBar.fillAmount = amount / maxHealth;
        }
    }

    public float GetAmoutOfHealth()
    {
        return amount;
    }

    private IEnumerator Blink()
    {
        entityAttack.SetCanAttack(false);

        for(float timer = 0 ; timer < blinkTime ; timer += Time.deltaTime)
        {
            graphics.SetActive(!graphics.activeInHierarchy);

            yield return null;
        }
        graphics.SetActive(true);

        entityAttack.SetCanAttack(true);
    }

    private IEnumerator KillEntity()
    {
        if(deathParticles != null)
        {
            deathParticles.Play();
        }
        yield return new WaitForSeconds(0.5f);
        Destroy(transform.parent.gameObject);
    }
}
