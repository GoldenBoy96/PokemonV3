using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleController : MonoBehaviour
{
    [SerializeField] Pokemon player1Pokemon;
    [SerializeField] Pokemon player2Pokemon;

    [SerializeField] Weather weather;
    [SerializeField] int weatherTurnLeft;

    [SerializeField] Hazard player1Hazard;
    [SerializeField] Hazard player2Hazard;


    public void AddTurnAction()
    {

    }
    public void ExecuteTurn()
    {
        //On enter turn event (weather...)

        //Pokemon move event

        //End turn event (burn, poison, leftover, ...)

    }

    public int CalculateDamage(Pokemon attacker, Pokemon target, MoveSO move)
    {
        float damage = 0;
        int level = attacker.Level;
        int power = move.MovePower;

        float A = 1; //attack effective base on move physical or special
        float D = 1; //defent effective base on move physical or special

        if (move.MoveCategory == MoveCategory.Physical)
        {
            A = attacker.GetBuffLevel(StatName.Atk);
            D = target.GetBuffLevel(StatName.Def);
        }
        else if (move.MoveCategory == MoveCategory.Special)
        {
            A = attacker.GetBuffLevel(StatName.SpA);
            D = target.GetBuffLevel(StatName.SpD);
        }

        float targets = 1;

        float weather = 1;
        if (this.weather == Weather.HarshSunlight)
        {
            if (move.MoveType == PokemonType.Fire)
            {
                weather = 1.5f;
            }
            else if (move.MoveType == PokemonType.Water)
            {
                weather = 0.5f;
            }
        }
        else if (this.weather == Weather.Rain)
        {
            if (move.MoveType == PokemonType.Fire)
            {
                weather = 0.5f;
            }
            else if (move.MoveType == PokemonType.Water)
            {
                weather = 1.5f;
            }
        }

        float critical = 1;
        float critRate = 6.25f;
        switch (attacker.GetBuffLevel(StatName.Crit))
        {
            case 1:
                critRate = 6.25f;
                break;
            case 2:
                critRate = 12.5f;
                break;
            case 3:
                critRate = 25f;
                break;
            case 4:
                critRate = 33.3f;
                break;
            case 5:
                critRate = 50f;
                break;
            default:
                critRate = 6.25f;
                break;
        }
        if (Random.Range(0, 100) < critRate)
        {
            critical = 1.5f;
        }

        float random = (Random.Range(85, 100)) / 100;

        float stab = 1;
        if (attacker.PokemonBaseSO.Type1 == move.MoveType || attacker.PokemonBaseSO.Type2 == move.MoveType)
        {
            stab = 1.5f;
        }


        float typeEffectiveness = PokemonUtil.GetTypeEffective(move.MoveType, target.PokemonBaseSO.Type1, target.PokemonBaseSO.Type2);

        float burn = 1f;
        if (attacker.Status == Status.Burned && move.MoveCategory == MoveCategory.Physical)
        {
            burn = 0.5f;
        }

        //in most case other == 1
        //other are affected by item or something else (life orb, choice, ...)
        float other = 1f;

        damage = (((((2 * level) / 5) + 2) * power * (A / D)) / 50 + 2)
            * targets * weather * critical * random * stab * typeEffectiveness * burn * other;

        return (int)damage;
    }

}

