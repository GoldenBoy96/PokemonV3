using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.Scripts.Trainer
{
    [Serializable]
    //[CreateAssetMenu(fileName = "Player", menuName = "ScriptableObjects/Player")]
    public class Player
    {
        [SerializeField] string playerName;
        [SerializeField] Pokemon[] party;
        [SerializeField] List<Pokemon> boxs;
        [SerializeField] Inventory inventory;
        [SerializeField] float cash;

        public string PlayerName { get => playerName; set => playerName = value; }
        public Pokemon[] Party { get => party; set => party = value; }
        public List<Pokemon> Boxs { get => boxs; set => boxs = value; }
        public Inventory Inventory { get => inventory; set => inventory = value; }
        public float Cash { get => cash; set => cash = value; }

        public override string ToString()
        {
            return $"{{{nameof(PlayerName)}={PlayerName}, {nameof(Party)}={Party}, {nameof(Boxs)}={Boxs}, {nameof(Inventory)}={Inventory}, {nameof(Cash)}={Cash.ToString()}}}";
        }
    }
}