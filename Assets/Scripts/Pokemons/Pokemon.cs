using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pokemon : MonoBehaviour
{
    private PokemonBaseSO pokemonBaseSO;

    private int level;
    private IVs ivs;
    private EVs evs;
    private Nature nature;
    private Gender gender;

    private int maxHp;
    private int hp;
    private int atk;
    private int def;
    private int spA;
    private int spD;
    private int spe;

    private AbilitySO ability;
    private List<LearnedMove> moveset;

    private int totaXp;
    private int toNextLevelXp;

    private ItemSO holdItem;

    private ItemSO pokeball;

    private List<Buff> buffList;
    private Status status = Status.None;

    public PokemonBaseSO PokemonBaseSO { get => pokemonBaseSO; set => pokemonBaseSO = value; }
    public int Level { get => level; set => level = value; }
    public IVs Ivs { get => ivs; set => ivs = value; }
    public EVs Evs { get => evs; set => evs = value; }
    public Nature Nature { get => nature; set => nature = value; }
    public Gender Gender { get => gender; set => gender = value; }
    public int MaxHp { get => maxHp; set => maxHp = value; }
    public int Hp { get => hp; set => hp = value; }
    public int Atk { get => atk; set => atk = value; }
    public int Def { get => def; set => def = value; }
    public int SpA { get => spA; set => spA = value; }
    public int SpD { get => spD; set => spD = value; }
    public int Spe { get => spe; set => spe = value; }
    public AbilitySO Ability { get => ability; set => ability = value; }
    public List<LearnedMove> Moveset { get => moveset; set => moveset = value; }
    public int TotaXp { get => totaXp; set => totaXp = value; }
    public int ToNextLevelXp { get => toNextLevelXp; set => toNextLevelXp = value; }
    public ItemSO HoldItem { get => holdItem; set => holdItem = value; }
    public ItemSO Pokeball { get => pokeball; set => pokeball = value; }
    public List<Buff> BuffList { get => buffList; set => buffList = value; }
    public Status Status { get => status; set => status = value; }

    public float GetBuffLevel(StatName stat)
    {
        foreach (var buff in BuffList)
        {
            if (buff.Stat == stat)
            {
                return buff.BuffLevel;
            }
        }
        return 1f;
    }
}

public struct LearnedMove
{
    MoveSO move;
    int currentPp;
    int maxPp;

    public MoveSO Move { get => move; set => move = value; }
    public int CurrentPp { get => currentPp; set => currentPp = value; }
    public int MaxPp { get => maxPp; set => maxPp = value; }
}

public enum Gender
{
    Male,
    Female
}

public struct Buff
{
    StatName stat;
    int buffLevel; //1 level = +0.5f * total

    public StatName Stat { get => stat; set => stat = value; }
    public int BuffLevel { get => buffLevel; set => buffLevel = value; }
}



