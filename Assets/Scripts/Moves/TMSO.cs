using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TM", menuName = "ScriptableObjects/Move/TM")]
public class TMSO : ScriptableObject
{
    [SerializeField] private int tmId;
    [SerializeField] private MoveSO move;

    public int TmId { get => tmId; set => tmId = value; }
    public MoveSO Move { get => move; set => move = value; }
}
