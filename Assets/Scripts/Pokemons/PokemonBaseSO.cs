using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Pokemon", menuName = "ScriptableObjects/Pokemon")]
public class PokemonBaseSO : ScriptableObject
{
    [SerializeField] private string pokemonName;
    [SerializeField] private int baseHp;
    [SerializeField] private int baseAtk;
    [SerializeField] private int baseDef;
    [SerializeField] private int baseSpA;
    [SerializeField] private int baseSpD;
    [SerializeField] private int baseSpe;
    [SerializeField] private List<Ability> abilities;
    [TextArea]
    [SerializeField] private string description;
    [SerializeField] private PokemonType type1;
    [SerializeField] private PokemonType type2;
    [SerializeField] private List<LevelUpMove> levelUpMoves;
    [SerializeField] private List<TMSO> tmMoves;
    [SerializeField] private List <HMSO> hmMoves;
    [SerializeField] private int pokedexNo;
    [SerializeField] private EvYield evYield;
    [SerializeField] private float catchRate;
    [SerializeField] private NextEvolution nextEvolution;
    [SerializeField] private GameObject backSprite;
    [SerializeField] private GameObject frontSprite;
    [SerializeField] private GameObject iconSprite;




    public string PokemonName { get => pokemonName; set => pokemonName = value; }
    public int BaseHp { get => baseHp; set => baseHp = value; }
    public int BaseAtk { get => baseAtk; set => baseAtk = value; }
    public int BaseDef { get => baseDef; set => baseDef = value; }
    public int BaseSpA { get => baseSpA; set => baseSpA = value; }
    public int BaseSpD { get => baseSpD; set => baseSpD = value; }
    public int BaseSpe { get => baseSpe; set => baseSpe = value; }
    public List<Ability> Abilities { get => abilities; set => abilities = value; }
    public List<LevelUpMove> LevelUpMoves { get => levelUpMoves; set => levelUpMoves = value; }
    public List<TMSO> TmMoves { get => tmMoves; set => tmMoves = value; }
    public List<HMSO> HmMoves { get => hmMoves; set => hmMoves = value; }
    public string Description { get => description; set => description = value; }
    public PokemonType Type1 { get => type1; set => type1 = value; }
    public PokemonType Type2 { get => type2; set => type2 = value; }
    public int PokedexNo { get => pokedexNo; set => pokedexNo = value; }
    public EvYield EvYield { get => evYield; set => evYield = value; }
    public float CatchRate { get => catchRate; set => catchRate = value; }
    public NextEvolution NextEvolution { get => nextEvolution; set => nextEvolution = value; }
    public GameObject BackSprite { get => backSprite; set => backSprite = value; }
    public GameObject FrontSprite { get => frontSprite; set => frontSprite = value; }
    public GameObject IconSprite { get => iconSprite; set => iconSprite = value; }

    public override string ToString()
    {
        return $"{{{nameof(PokemonName)}={PokemonName}, {nameof(BaseHp)}={BaseHp.ToString()}, {nameof(BaseAtk)}={BaseAtk.ToString()}, {nameof(BaseDef)}={BaseDef.ToString()}, {nameof(BaseSpA)}={BaseSpA.ToString()}, {nameof(BaseSpD)}={BaseSpD.ToString()}, {nameof(BaseSpe)}={BaseSpe.ToString()}, {nameof(Abilities)}={Abilities}, {nameof(LevelUpMoves)}={LevelUpMoves}, {nameof(TmMoves)}={TmMoves}, {nameof(HmMoves)}={HmMoves}, {nameof(Description)}={Description}, {nameof(Type1)}={Type1.ToString()}, {nameof(Type2)}={Type2.ToString()}, {nameof(PokedexNo)}={PokedexNo.ToString()}, {nameof(EvYield)}={EvYield.ToString()}, {nameof(CatchRate)}={CatchRate.ToString()}, {nameof(NextEvolution)}={NextEvolution.ToString()}, {nameof(BackSprite)}={BackSprite}, {nameof(FrontSprite)}={FrontSprite}, {nameof(IconSprite)}={IconSprite}}}";
    }
}

[Serializable]
public struct Ability
{
    [SerializeField] private AbilitySO abilitySO;
    [SerializeField] private bool isHidden;
}

[Serializable]
public struct LevelUpMove
{
    [SerializeField] private MoveSO moveSO;
    [SerializeField] private int level;

    public MoveSO MoveSO { get => moveSO; set => moveSO = value; }
    public int Level { get => level; set => level = value; }
}

[Serializable]
public struct EvYield
{
    [SerializeField] private int amount;
    [SerializeField] private Stats stat;

    public int Amount { get => amount; set => amount = value; }
    public Stats Stat { get => stat; set => stat = value; }
}


[Serializable]
public struct NextEvolution
{
    //if pokemonBaseSO == null, this pokemon has no evolution
    //if pokemonBaseSO != null and level == -1, this pokemon has evolution without level
    [SerializeField] private PokemonBaseSO pokemonBaseSO;
    [SerializeField] private int level;
}