using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "ScriptableObjects/Ability")]
public class AbilitySO : ScriptableObject
{
    [SerializeField] string abilityName;
    [TextArea]
    [SerializeField] string abilityDescription;
    [SerializeField] GameObject abilityEffect;

    public string AbilityName { get => abilityName; set => abilityName = value; }
    public string AbilityDescription { get => abilityDescription; set => abilityDescription = value; }
    public GameObject AbilityEffect { get => abilityEffect; set => abilityEffect = value; }
}
