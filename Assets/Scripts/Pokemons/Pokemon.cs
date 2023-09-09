using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class Pokemon
{
    [SerializeField] private PokemonBaseSO pokemonBaseSO;

    [SerializeField] private String nameString;
    [SerializeField] private int level = 1;
    [SerializeField] private IVs ivs = new();
    [SerializeField] private EVs evs = new();
    [SerializeField] private Nature nature = Nature.Adamant;
    [SerializeField] private Gender gender = Gender.Male;

    private int maxHp;
    private int hp;
    private int atk;
    private int def;
    private int spA;
    private int spD;
    private int spe;

    [SerializeField] private AbilitySO ability;
    [SerializeField] private List<LearnedMove> moveset = new();

    private int totalXp;
    private int toNextLevelXp;

    [SerializeField] private ItemSO holdItem;

    [SerializeField] private ItemSO pokeball;

    [SerializeField] private List<Buff> buffList = new();
    [SerializeField] private Status status = Status.None;

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
    public int TotalXp { get => totalXp; set => totalXp = value; }
    public int ToNextLevelXp { get => toNextLevelXp; set => toNextLevelXp = value; }
    public ItemSO HoldItem { get => holdItem; set => holdItem = value; }
    public ItemSO Pokeball { get => pokeball; set => pokeball = value; }
    public List<Buff> BuffList { get => buffList; set => buffList = value; }
    public Status Status { get => status; set => status = value; }
    public string NameString { get => nameString; set => nameString = value; }

    public Pokemon()
    {
        //ivs ??= new();
        //evs ??= new();


    }

    public Pokemon(PokemonBaseSO pokemonBaseSO, int level)
    {
        this.pokemonBaseSO = pokemonBaseSO;
        this.level = level;
        int levelMoveIndex = 0;
        int randomLearnableMove = 0;
        for (int i = 0; i < pokemonBaseSO.LevelUpMoves.Count; i++)
        {
            if (level >= pokemonBaseSO.LevelUpMoves[i].Level)
            {
                levelMoveIndex = i;
            }
        }

        if (levelMoveIndex < 4)
        {
            for (int i = 0; i < levelMoveIndex; i++)
            {
                moveset.Add(new LearnedMove(pokemonBaseSO.LevelUpMoves[i].MoveSO));
            }
        }
        else
        {
            List<int> learnedMoveIndex = new();
            for (int i = 0; i < 4; i++)
            {
                do
                {
                    randomLearnableMove = Random.Range(0, levelMoveIndex);
                } while (learnedMoveIndex.Contains(randomLearnableMove));

                moveset.Add(new LearnedMove(pokemonBaseSO.LevelUpMoves[randomLearnableMove].MoveSO));
            }
        }
        InitStats();
    }

    public void InitStats()
    {
        if (NameString == null || NameString.Trim().Equals(""))
        {
            this.nameString = pokemonBaseSO.PokemonName;
        }

        MaxHp = CalculateStat(Stats.Hp, PokemonBaseSO.BaseHp, Level, Ivs.Hp, Evs.Hp);
        Hp = MaxHp;
        Atk = CalculateStat(Stats.Atk, PokemonBaseSO.BaseAtk, Level, Ivs.Atk, Evs.Atk);
        Def = CalculateStat(Stats.Def, PokemonBaseSO.BaseDef, Level, Ivs.Def, Evs.Def);
        SpA = CalculateStat(Stats.SpA, PokemonBaseSO.BaseSpA, Level, Ivs.SpA, Evs.SpA);
        SpD = CalculateStat(Stats.SpD, PokemonBaseSO.BaseSpD, Level, Ivs.SpD, Evs.SpD);
        Spe = CalculateStat(Stats.Spe, PokemonBaseSO.BaseSpe, Level, Ivs.Spe, Evs.Spe);
    }

    public void UpdateStats()
    {
        MaxHp = CalculateStat(Stats.Hp, PokemonBaseSO.BaseHp, Level, Ivs.Hp, Evs.Hp);
        Atk = CalculateStat(Stats.Atk, PokemonBaseSO.BaseAtk, Level, Ivs.Atk, Evs.Atk);
        Def = CalculateStat(Stats.Def, PokemonBaseSO.BaseDef, Level, Ivs.Def, Evs.Def);
        SpA = CalculateStat(Stats.SpA, PokemonBaseSO.BaseSpA, Level, Ivs.SpA, Evs.SpA);
        SpD = CalculateStat(Stats.SpD, PokemonBaseSO.BaseSpD, Level, Ivs.SpD, Evs.SpD);
        Spe = CalculateStat(Stats.Spe, PokemonBaseSO.BaseSpe, Level, Ivs.Spe, Evs.Spe);
    }



    public float GetBuffLevel(Stats stat)
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

    private int CalculateStat(Stats stats, int baseStat, int level, int iv, int ev)
    {
        int value = 0;
        if (stats.Equals(Stats.Hp))
        {
            value = ((2 * baseStat + iv + (ev / 4) * level) * level / 100) + level + 10;
        }
        else
        {
            value = (((2 * baseStat + iv + (ev / 4) * level) * level / 100) + 5);

        }

        return value;
    }

    public override string ToString()
    {
        return $"{{{nameof(PokemonBaseSO)}={PokemonBaseSO.PokemonName}, {nameof(Level)}={Level.ToString()}, {nameof(Ivs)}={Ivs}, {nameof(Evs)}={Evs}, {nameof(Nature)}={Nature.ToString()}, {nameof(Gender)}={Gender.ToString()}, {nameof(MaxHp)}={MaxHp.ToString()}, {nameof(Hp)}={Hp.ToString()}, {nameof(Atk)}={Atk.ToString()}, {nameof(Def)}={Def.ToString()}, {nameof(SpA)}={SpA.ToString()}, {nameof(SpD)}={SpD.ToString()}, {nameof(Spe)}={Spe.ToString()}, {nameof(Ability)}={Ability}, {nameof(Moveset)}={Moveset}, {nameof(TotalXp)}={TotalXp.ToString()}, {nameof(ToNextLevelXp)}={ToNextLevelXp.ToString()}, {nameof(HoldItem)}={HoldItem}, {nameof(Pokeball)}={Pokeball}, {nameof(BuffList)}={BuffList}, {nameof(Status)}={Status.ToString()}}}";
    }
}

[Serializable]
public struct LearnedMove
{
    [SerializeField] MoveSO move;
    [SerializeField] int currentPp;
    [SerializeField] int maxPp;

    public LearnedMove(MoveSO move) : this()
    {
        this.move = move;
        maxPp = move.MovePp;
        currentPp = maxPp;
    }
    public LearnedMove(MoveSO move, int currentPp, int maxPp)
    {
        this.move = move;
        this.currentPp = currentPp;
        this.maxPp = maxPp;
    }

    public MoveSO Move { get => move; set => move = value; }
    public int CurrentPp { get => currentPp; set => currentPp = value; }
    public int MaxPp { get => maxPp; set => maxPp = value; }
}





public struct Buff
{
    Stats stat;
    int buffLevel; //1 level = +0.5f * total

    public Stats Stat { get => stat; set => stat = value; }
    public int BuffLevel { get => buffLevel; set => buffLevel = value; }
}



