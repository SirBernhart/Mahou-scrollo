using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpecialEffect { none, knockback, stun }
public enum ActionType { none, lightMelee, heavyMelee, lightRanged, heavyRanged, transformation }

[CreateAssetMenu(fileName = "ComboElement", menuName = "ScriptableObjects/Combo Element")]
public class ComboElement : ScriptableObject
{
    public ComboElement[] nextElements;
    public SpecialEffect specialEffect;
    public float specialEffectIntensity;
    public ActionType actionToTrigger;

    public float cooldown;
    public int damage;
    public bool isFinisher;
}
