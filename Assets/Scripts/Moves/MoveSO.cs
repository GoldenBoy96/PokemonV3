using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Move", menuName = "ScriptableObjects/Move/Move")]
public class MoveSO : ScriptableObject
{
    [SerializeField] private string moveName;
    [SerializeField] private PokemonType moveType;
    [SerializeField] private int movePower;
    [SerializeField] private int moveAccuracy;
    [SerializeField] private float movePp;
    [SerializeField] private bool isMakeContact;
    [SerializeField] private int movePriority;
    [SerializeField] private MoveCategory moveCategory;
    [TextArea]
    [SerializeField] private string moveDescription;
    private IMoveEffect moveEffect;
    [SerializeField] private GameObject moveEffectObject;
    [SerializeField] private GameObject sprite;

    public string MoveName { get => moveName; set => moveName = value; }
    public PokemonType MoveType { get => moveType; set => moveType = value; }
    public int MovePower { get => movePower; set => movePower = value; }
    public float MovePp { get => movePp; set => movePp = value; }
    public bool IsMakeContact { get => isMakeContact; set => isMakeContact = value; }
    public int MovePriority { get => movePriority; set => movePriority = value; }
    public int MoveAccuracy { get => moveAccuracy; set => moveAccuracy = value; }
    public MoveCategory MoveCategory { get => moveCategory; set => moveCategory = value; }
    public string MoveDescription { get => moveDescription; set => moveDescription = value; }
    public IMoveEffect MoveEffect { get => moveEffect; set => moveEffect = moveEffectObject.GetComponent<IMoveEffect>(); }
    public GameObject MoveEffectObject { get => moveEffectObject; set => moveEffectObject = value; }
    public GameObject Sprite { get => sprite; set => sprite = value; }
}

public enum MoveCategory
{
    Physical, Special, Status
}