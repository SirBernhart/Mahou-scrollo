using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int amount;
    [SerializeField] private GameObject graphics;
    [SerializeField] private float blinkTime;
    [SerializeField] private EntityAttack entityAttack;

    public void ReduceHealth(int amount)
    {
        this.amount -= amount;
        Debug.Log("Ouch! Current health: " + this.amount);

        StartCoroutine(Blink());

        if(this.amount <= 0)
        {
            StartCoroutine(KillEntity());
        } 
    }

    public void IncreaseHealth(int amount)
    {
        this.amount += amount;
        Debug.Log("Whew! Current health: " + this.amount);
    }

    public int GetAmoutOfHealth()
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
        yield return new WaitForSeconds(0.5f);
        Destroy(transform.parent.gameObject);
    }
}
