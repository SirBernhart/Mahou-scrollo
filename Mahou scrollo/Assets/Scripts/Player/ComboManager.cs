using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboManager : MonoBehaviour
{
    [SerializeField] private float comboResetTime;
    private float comboTimer = 0;
    private Coroutine comboTimerCoroutine;

    //private ComboElement[] currentCombo = new ComboElement[5];
    private ComboElement currentElement;

    public void AddToCombo(ComboElement newElement)
    {
        if (newElement.isFinisher)
        {
            ClearCombo();
        }
        else
        {
            if (comboTimerCoroutine == null)
                StartCoroutine(StartComboResetTimer());

            if (currentElement != null && CheckIfNewElementMatchesNextElements(newElement))
            {
                currentElement = newElement;
            }
            else
            {
                StopCoroutine(comboTimerCoroutine);
                ClearCombo();
            }
            comboTimer = 0;
        }
    }

    private bool CheckIfNewElementMatchesNextElements(ComboElement newElement)
    {
        for(int i = 0 ; i < currentElement.nextElements.Length ; ++i)
        {
            if (currentElement.nextElements[i].Equals(newElement))
                return true;
        }

        return false;
    }

    private void ClearCombo()
    {
        currentElement = null;
    }

    private IEnumerator StartComboResetTimer()
    {
        for(; comboTimer < comboResetTime ; comboTimer += Time.deltaTime)
        {
            yield return null;  
        }

        comboTimer = 0;
        ClearCombo();
    }
}
