using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformationController : MonoBehaviour
{
    [SerializeField] private Sprite normalSprite;
    [SerializeField] private Sprite magicSprite;
    [SerializeField] private SpriteRenderer currentSpriteRenderer;

    private bool isInNormalForm = true;

    public bool GetIsInNormalForm() { return isInNormalForm; }

    public void Transform()
    {
        if (isInNormalForm)
        {
            currentSpriteRenderer.sprite = magicSprite;
            isInNormalForm = false;
        }
        else
        {
            currentSpriteRenderer.sprite = normalSprite;
            isInNormalForm = true;
        }
    }
}
