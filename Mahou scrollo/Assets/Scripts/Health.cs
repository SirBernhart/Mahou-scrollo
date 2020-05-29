using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float amount;
    [SerializeField] private ParticleSystem deathParticles;
    [SerializeField] private SpriteRenderer graphics;
    [SerializeField] private float blinkTime;

    [SerializeField] private Image healthBar;
    [SerializeField] private GameObject restart;

    [SerializeField] private float flinchTime;
    private bool canAct = true;
    public bool GetCanAct() { return canAct; }

    private void Start()
    {
        amount = maxHealth;
    }

    public void ReduceHealth(int amount)
    {
        this.amount -= amount;
        UpdateHealthBarGraphics();

        StartCoroutine(Flinch());
        StartCoroutine(Blink());

        if(this.amount <= 0)
        {
            if (transform.root.tag == "Player")
            {
                restart.SetActive(true);
            }
            StopAllCoroutines();
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
        Color bleakColor = graphics.color;
        Color transparentColor = new Color(bleakColor.r, bleakColor.g, bleakColor.b, 50f);

        WaitForSeconds timeBetweenBlinks = new WaitForSeconds(0.1f);

        bool isTransparent = false;
        for (float timer = 0 ; timer < blinkTime ; timer += Time.deltaTime)
        {
            if (isTransparent)
                graphics.material.color = bleakColor;
            else
                graphics.material.color = transparentColor;

            isTransparent = !isTransparent;

            yield return null;
        }
        graphics.material.color = bleakColor;
    }

    private IEnumerator Flinch()
    {
        canAct = false;
        yield return new WaitForSeconds(flinchTime);
        canAct = true;
    }

    private IEnumerator KillEntity()
    {
        canAct = false;

        if(deathParticles != null)
        {
            deathParticles.Play();
        }
        yield return new WaitForSeconds(0.5f);
  
        Destroy(transform.parent.gameObject);
    }
}
