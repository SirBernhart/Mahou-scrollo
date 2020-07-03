using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformationController : MonoBehaviour
{
    [SerializeField] private GameObject normalForm;
    [SerializeField] private GameObject magicForm;
    [SerializeField] private Health health;
    [SerializeField] private GameObject normalFormPiecesParent;
    [SerializeField] private GameObject magicFormPiecesParent;
    private SpriteRenderer[] normalFormPieces;
    private SpriteRenderer[] magicFormPieces;

    private void Start()
    {
        normalFormPieces = normalFormPiecesParent.GetComponentsInChildren<SpriteRenderer>();
        magicFormPieces = magicFormPiecesParent.GetComponentsInChildren<SpriteRenderer>();

        ChangeSpriteRenderers(normalFormPieces, magicFormPieces);
        health.graphics = normalFormPieces;
    }

    private bool isInNormalForm = true;

    public bool GetIsInNormalForm() { return isInNormalForm; }

    public void Transform()
    {
        if (isInNormalForm)
        {
            ChangeSpriteRenderers(magicFormPieces, normalFormPieces);
            health.graphics = magicFormPieces;

            isInNormalForm = false;
        }
        else
        {
            ChangeSpriteRenderers(normalFormPieces, magicFormPieces);
            health.graphics = normalFormPieces;

            isInNormalForm = true;
        }
    }

    private void ChangeSpriteRenderers(SpriteRenderer[] rendererToEnable, SpriteRenderer[] rendererToDisable)
    {
        for(int i = 0 ; i < rendererToEnable.Length ; ++i)
        {
            rendererToEnable[i].enabled = true;
        }
        for (int i = 0; i < rendererToDisable.Length; ++i)
        {
            rendererToDisable[i].enabled = false;
        }
    }
}
