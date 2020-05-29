using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpecialEffect { none, knockback, stun }

[CreateAssetMenu(fileName = "ComboElement", menuName = "ScriptableObjects/Combo Element")]
public class ComboElement : ScriptableObject
{
    public ComboElement[] nextElements;
    public SpecialEffect specialEffect;

    public int damage;
    public bool isFinisher;
}
