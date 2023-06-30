using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HM", menuName = "ScriptableObjects/Move/HM")]
public class HMSO : ScriptableObject
{
    [SerializeField] private int hmId;
    [SerializeField] private MoveSO move;

    public int HmId { get => hmId; set => hmId = value; }
    public MoveSO Move { get => move; set => move = value; }
}
