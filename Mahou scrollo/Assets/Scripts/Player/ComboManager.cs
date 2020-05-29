using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboManager : MonoBehaviour
{
    [SerializeField] private float comboResetTime;
    private float comboTimer = 0;
    private Coroutine comboTimerCoroutine;

    [SerializeField] private List<ComboElement> possibleComboElements;
    private ComboElement currentElement;

    public ComboElement AddToCombo(ActionType actionType)
    {
        ComboElement newElement = TranslateActionToComboElement(actionType);
        // Didn't find a possible element to follow up the current action. Will return a basic action
        if (newElement == null)
        {
            ClearCombo();
            return TranslateActionToComboElement(actionType);
        }
        else if (newElement.isFinisher)
        {
            StartCoroutine(DelayClearingCombo());
            return newElement;
        }
        if (comboTimerCoroutine == null)
            StartCoroutine(StartComboResetTimer());

        currentElement = newElement;

        return currentElement;
    }

    private ComboElement TranslateActionToComboElement(ActionType actionType)
    {
        if(currentElement == null)
        {
            switch (actionType)
            {
                case ActionType.lightMelee:
                    currentElement = FindElementOfThisName("Jab");
                    break;

                case ActionType.heavyMelee:
                    currentElement = FindElementOfThisName("Kick");
                    break;

                case ActionType.lightRanged:
                    currentElement = FindElementOfThisName("SunBolt");
                    break;
            }
            return currentElement;
        }
        else
        {
            foreach(ComboElement nextElement in currentElement.nextElements)
            {
                if (nextElement.actionToTrigger == actionType)
                    return nextElement;
            }
        }
        return null;
    }

    private ComboElement FindElementOfThisName (string elementName)
    {
        foreach(ComboElement element in possibleComboElements)
        {
            if (element.name == elementName)
                return element;
        }
        return null;
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
        if(comboTimerCoroutine != null)
            StopCoroutine(comboTimerCoroutine);
        currentElement = null;
        comboTimer = 0;
    }

    private IEnumerator DelayClearingCombo()
    {
        yield return new WaitForEndOfFrame();
        ClearCombo();
    }

    private IEnumerator StartComboResetTimer()
    {
        for(comboTimer = 0 ; comboTimer < comboResetTime ; comboTimer += Time.deltaTime)
        {
            yield return null;  
        }

        comboTimerCoroutine = null;
        comboTimer = 0;
        ClearCombo();
    }
}
