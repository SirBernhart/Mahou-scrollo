using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float amount;
    [SerializeField] private ParticleSystem deathParticles;
    public SpriteRenderer[] graphics;
    [SerializeField] private float blinkTimes;

    [SerializeField] private Image healthBar;
    [SerializeField] private GameObject restart;

    public Score score;

    [SerializeField] private float flinchTime;
    private bool canAct = true;
    private bool isDying = false;
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

        if(this.amount <= 0 && !isDying)
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
        Color bleakColor = Color.white;
        Color transparentColor = new Color(bleakColor.r, bleakColor.g, bleakColor.b, 50f);

        WaitForSeconds timeBetweenBlinks = new WaitForSeconds(0.1f);

        bool isTransparent = false;
            for (float times = 0 ; times < blinkTimes ; ++times)
            {
                if (isTransparent)
                    foreach (SpriteRenderer piece in graphics)
                    {
                        piece.material.color = bleakColor;
                    }
                else
                    foreach (SpriteRenderer piece in graphics)
                    {
                        piece.material.color = transparentColor;
                    }

                isTransparent = !isTransparent;

                yield return timeBetweenBlinks;
            }
        foreach (SpriteRenderer piece in graphics)
        {
            piece.material.color = bleakColor;
        }
    }

    private IEnumerator Flinch()
    {
        canAct = false;
        yield return new WaitForSeconds(flinchTime);
        canAct = true;
    }

    private IEnumerator KillEntity()
    {
        isDying = true;
        canAct = false;

        if(deathParticles != null)
        {
            deathParticles.Play();
        }

        if(score != null)
            score.IncreaseScore(15);

        yield return new WaitForSeconds(0.5f);
  
        Destroy(transform.parent.gameObject);
    }
}
