using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformationController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sprite;

    private Color magicColor = new Color(0, 0, 255);
    private Color normalColor = new Color(250, 0, 0);

    private bool isInNormalForm = true;

    public void Transform()
    {
        if (isInNormalForm)
        {
            sprite.color = magicColor;
            isInNormalForm = false;
        }
        else
        {
            sprite.color = normalColor;
            isInNormalForm = true;
        }
    }
}
