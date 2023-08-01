using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSO : ScriptableObject
{
    [SerializeField] string itemName;
    [SerializeField] string description;
    [SerializeField] GameObject icon;

    public string ItemName { get => itemName; set => itemName = value; }
    public string Description { get => description; set => description = value; }
    public GameObject Icon { get => icon; set => icon = value; }
}

public enum ItemCategory
{
    BattleItems, Berries, GeneralItems, HoldItems, Machines, Medicine, Pokeballs
}
