using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformationController : MonoBehaviour
{

    [SerializeField] private GameObject normalSprite;
    [SerializeField] private GameObject magicSprite;

    private bool isInNormalForm = true;

    public void Transform()
    {
        if (isInNormalForm)
        {
            normalSprite.SetActive(false);
            magicSprite.SetActive(true);
            isInNormalForm = false;
        }
        else
        {
            magicSprite.SetActive(false);
            normalSprite.SetActive(true);
            isInNormalForm = true;
        }
    }
}
