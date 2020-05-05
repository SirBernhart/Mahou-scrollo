using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int amount;

    public void ReduceHealth(int amount)
    {
        this.amount -= amount;
        Debug.Log("Ouch! Current health: " + this.amount);

        if(this.amount <= 0)
        {
            Destroy(transform.parent.gameObject);
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
}
