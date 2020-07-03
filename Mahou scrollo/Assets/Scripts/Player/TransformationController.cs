using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransformationController : MonoBehaviour
{
    [SerializeField] private Health health;
    [SerializeField] private EntityMovement entityMovement;
    [SerializeField] private GameObject normalFormPiecesParent;
    [SerializeField] private GameObject magicFormPiecesParent;
    private SpriteRenderer[] normalFormPieces;
    private SpriteRenderer[] magicFormPieces;
    [SerializeField] private Animator animatorNormal;
    [SerializeField] private Animator animatorMagic;
    [SerializeField] private Image portrait;
    [SerializeField] private Sprite minaNormal;
    [SerializeField] private Sprite minaMagica;

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
            health.animator = animatorMagic;
            entityMovement.animator = animatorMagic;

            isInNormalForm = false;
        }
        else
        {
            ChangeSpriteRenderers(normalFormPieces, magicFormPieces);
            health.graphics = normalFormPieces;
            health.animator = animatorNormal;
            entityMovement.animator = animatorNormal;

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
