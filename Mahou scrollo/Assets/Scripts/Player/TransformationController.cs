using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformationController : MonoBehaviour
{
    [SerializeField] private GameObject normalForm;
    [SerializeField] private GameObject magicForm;
    [SerializeField] private Health health;
    [SerializeField] private SpriteRenderer[] normalFormPieces;
    [SerializeField] private SpriteRenderer[] magicFormPieces;

    private bool isInNormalForm = true;

    public bool GetIsInNormalForm() { return isInNormalForm; }

    public void Transform()
    {
        if (isInNormalForm)
        {
            health.graphics = magicFormPieces;
            normalForm.SetActive(false);
            magicForm.SetActive(true);

            isInNormalForm = false;
        }
        else
        {
            health.graphics = normalFormPieces;
            normalForm.SetActive(true);
            magicForm.SetActive(false);

            isInNormalForm = true;
        }
    }
}
