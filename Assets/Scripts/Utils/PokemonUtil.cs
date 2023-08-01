using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.XR;


public class PokemonUtil
{
    private static Dictionary<PokemonType, int> typeDictionary = new()
    {
        {PokemonType.Normal, 0},
        {PokemonType.Fire, 1},
        {PokemonType.Water, 2},
        {PokemonType.Electric, 3},
        {PokemonType.Grass, 4},
        {PokemonType.Ice, 5},
        {PokemonType.Fighting, 6},
        {PokemonType.Poison, 7},
        {PokemonType.Ground, 8},
        {PokemonType.Flying, 9},
        {PokemonType.Psychic, 10},
        {PokemonType.Bug, 11},
        {PokemonType.Rock, 12},
        {PokemonType.Ghost, 13},
        {PokemonType.Dragon, 14},
        {PokemonType.Dark, 15},
        {PokemonType.Steel, 16},
        {PokemonType.Fairy, 17},
    };

    public static readonly float[,] TypeChart = new float[18, 18] {
    //             Nor   Fir   Wat   Ele   Gra   Ice   Fig   Poi   Gro   Fly   Psy   Bug   Roc   Gho   Dra   Dar   Ste   Fai
    /* Normal */  {1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 0.5f, 0.0f, 1.0f, 1.0f, 0.5f, 1.0f},
    /* Fire */    {1.0f, 0.5f, 0.5f, 1.0f, 2.0f, 2.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 2.0f, 0.5f, 1.0f, 0.5f, 1.0f, 2.0f, 1.0f},
    /* Water */   {1.0f, 2.0f, 0.5f, 1.0f, 0.5f, 1.0f, 1.0f, 1.0f, 2.0f, 1.0f, 1.0f, 1.0f, 2.0f, 1.0f, 0.5f, 1.0f, 1.0f, 1.0f},
    /* Electric */{1.0f, 1.0f, 2.0f, 0.5f, 0.5f, 1.0f, 1.0f, 1.0f, 0.0f, 2.0f, 1.0f, 1.0f, 1.0f, 1.0f, 0.5f, 1.0f, 1.0f, 1.0f},
    /* Grass */   {1.0f, 0.5f, 2.0f, 1.0f, 0.5f, 1.0f, 1.0f, 0.5f, 2.0f, 0.5f, 1.0f, 0.5f, 2.0f, 1.0f, 0.5f, 1.0f, 0.5f, 1.0f},
    /* Ice */     {1.0f, 0.5f, 0.5f, 1.0f, 2.0f, 0.5f, 1.0f, 1.0f, 2.0f, 2.0f, 1.0f, 1.0f, 1.0f, 1.0f, 2.0f, 1.0f, 0.5f, 1.0f},
    /* Fighting */{2.0f, 1.0f, 1.0f, 1.0f, 1.0f, 2.0f, 1.0f, 0.5f, 1.0f, 0.5f, 0.5f, 0.5f, 2.0f, 0.0f, 1.0f, 2.0f, 2.0f, 0.5f},
    /* Poison */  {1.0f, 1.0f, 1.0f, 1.0f, 2.0f, 1.0f, 1.0f, 0.5f, 0.5f, 1.0f, 1.0f, 1.0f, 0.5f, 0.5f, 1.0f, 1.0f, 0.0f, 2.0f},
    /* Ground */  {1.0f, 2.0f, 1.0f, 2.0f, 0.5f, 1.0f, 1.0f, 2.0f, 1.0f, 0.0f, 1.0f, 0.5f, 2.0f, 1.0f, 1.0f, 1.0f, 2.0f, 1.0f},
    /* Flying */  {1.0f, 1.0f, 1.0f, 0.5f, 2.0f, 1.0f, 2.0f, 1.0f, 1.0f, 1.0f, 1.0f, 2.0f, 0.5f, 1.0f, 1.0f, 1.0f, 0.5f, 1.0f},
    /* Psychic*/  {1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 2.0f, 2.0f, 1.0f, 1.0f, 0.5f, 1.0f, 1.0f, 1.0f, 1.0f, 0.0f, 0.5f, 1.0f},
    /* Bug */     {1.0f, 0.5f, 1.0f, 1.0f, 2.0f, 1.0f, 0.5f, 0.5f, 1.0f, 0.5f, 2.0f, 1.0f, 1.0f, 0.5f, 1.0f, 2.0f, 0.5f, 0.5f},
    /* Rock */    {1.0f, 2.0f, 1.0f, 1.0f, 1.0f, 2.0f, 0.5f, 1.0f, 0.5f, 2.0f, 1.0f, 2.0f, 1.0f, 1.0f, 1.0f, 1.0f, 0.5f, 1.0f},
    /* Ghost */   {0.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 2.0f, 1.0f, 1.0f, 2.0f, 1.0f, 0.5f, 1.0f, 1.0f},
    /* Dragon */  {1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 2.0f, 1.0f, 0.5f, 0.0f},
    /* Dark */    {1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 0.5f, 1.0f, 1.0f, 1.0f, 2.0f, 1.0f, 1.0f, 2.0f, 1.0f, 0.5f, 1.0f, 0.5f},
    /* Steel */   {1.0f, 0.5f, 0.5f, 0.5f, 1.0f, 2.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 2.0f, 1.0f, 1.0f, 1.0f, 0.5f, 2.0f},
    /* Fairy */   {1.0f, 0.5f, 1.0f, 1.0f, 1.0f, 1.0f, 2.0f, 0.5f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 2.0f, 2.0f, 0.5f, 1.0f}
    };

    public static float GetTypeEffective(PokemonType attackType, PokemonType defendType)
    {
        return TypeChart[typeDictionary[attackType], typeDictionary[defendType]];
    }

    public static float GetTypeEffective(PokemonType attackType, PokemonType defendType1, PokemonType defendType2)
    {
        if (defendType1.Equals(defendType2))
        {
            return GetTypeEffective(attackType, defendType1);
        }

        return GetTypeEffective(attackType, defendType1) * GetTypeEffective(attackType, defendType2);
    }

    public static readonly Nature[,] NatureChart = new Nature[5, 5] {
        //                 -Atk            -Def           -SpA            -SpD            -Spe
        /* +Atk */  {Nature.Hardy , Nature.Lonely, Nature.Adamant, Nature.Naughty, Nature.Brave  },
        /* +Def */  {Nature.Bold  , Nature.Docile, Nature.Impish , Nature.Lax    , Nature.Relaxed},
        /* +SpA */  {Nature.Modest, Nature.Mild  , Nature.Bashful, Nature.Rash   , Nature.Quiet  },
        /* +SpD */  {Nature.Calm  , Nature.Gentle, Nature.Careful, Nature.Quirky , Nature.Sassy  },
        /* +Spe */  {Nature.Timid , Nature.Hasty , Nature.Jolly  , Nature.Naive  , Nature.Serious},
    };

}

public enum PokemonType
{
    None,
    Normal,
    Fire,
    Water,
    Electric,
    Grass,
    Ice,
    Fighting,
    Poison,
    Ground,
    Flying,
    Psychic,
    Bug,
    Rock,
    Ghost,
    Dragon,
    Dark,
    Steel,
    Fairy
}

public enum Status
{
    None,
    All,
    Burned,
    Posioned,
    BadlyPoisoned,
    Sleep,
    Paralysis,
    Freeze,

}

public enum Gender
{
    Male,
    Female,
    Unknown
}

public enum Nature
{
    Adamant,
    Bashful,
    Bold,
    Brave,
    Calm,
    Careful,
    Docile,
    Gentle,
    Hardy,
    Hasty,
    Impish,
    Jolly,
    Lax,
    Lonely,
    Mild,
    Modest,
    Naive,
    Naughty,
    Quiet,
    Quirky,
    Rash,
    Relaxed,
    Sassy,
    Serious,
    Timid,
}

public enum Stats
{
    Hp, Atk, Def, SpA, SpD, Spe, Crit, Acc
}

public enum Weather
{
    Normal, HarshSunlight, Rain, Sandstorm, Hail, Fog
}

//Hazards will be triggered when a pokemon enter the battle field
public enum Hazard 
{
    None, Spikes, PointedStones, PoisonSpikes, StickyWeb, SharpStell
}
