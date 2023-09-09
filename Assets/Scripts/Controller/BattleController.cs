using Assets.Scripts.Trainer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleController : MonoBehaviour
{
    public static BattleController Instance { get; private set; }

    [SerializeField] Player player1;
    [SerializeField] NPCSO player2;

    public Pokemon player1Pokemon;
    public Pokemon player2Pokemon;

    public GameObject player1PokemonSprite;
    public GameObject player2PokemonSprite;

    [SerializeField] Weather weather;
    [SerializeField] int weatherTurnLeft;

    [SerializeField] Hazard player1Hazard;
    [SerializeField] Hazard player2Hazard;

    private List<TurnMove> turnMoveList = new();

    public Player Player1 { get => player1; set => player1 = value; }
    public NPCSO Player2 { get => player2; set => player2 = value; }
    public Pokemon Player1Pokemon { get => player1Pokemon; set => player1Pokemon = value; }
    public Pokemon Player2Pokemon { get => player2Pokemon; set => player2Pokemon = value; }
    public Weather Weather { get => weather; set => weather = value; }
    public int WeatherTurnLeft { get => weatherTurnLeft; set => weatherTurnLeft = value; }
    public Hazard Player1Hazard { get => player1Hazard; set => player1Hazard = value; }
    public Hazard Player2Hazard { get => player2Hazard; set => player2Hazard = value; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

    }
    private void Start()
    {
        //SetOnFieldPokemon(ref player1.Party[0], ref player2.Party[0]);//==================================
        //player1Pokemon = player1.Party[0];
        player1Pokemon.InitStats();

        //Debug.Log("***" + player1Pokemon + " || " + player1.Party[0]);

        //player2Pokemon = player2.Party[0];
        player2Pokemon.InitStats();

        //player1Pokemon.Hp -= 10;
        //Debug.Log("***" + player1Pokemon);
        //Debug.Log("***" + player1.Party[0]);
        
    }

    public void HandleUpdate()
    {
       
    }

    public void StartWildPokemonBattle(Pokemon pokemon)
    {
        SetOnFieldPokemon(ref player1.Party[0], ref pokemon);
        //player2Pokemon = pokemon;
        player2 = null;
        BattleUI.Instance.StartBattle();
    }
    private void SetOnFieldPokemon(ref Pokemon player1Pokemon, ref Pokemon player2Pokemon)
    {
        Player1Pokemon = player1Pokemon;
        Player2Pokemon = player2Pokemon;
        ChangePlayer1PokemonSprite(player1Pokemon.PokemonBaseSO);
        ChangePlayer2PokemonSprite(player2Pokemon.PokemonBaseSO);

    }

    private void ChangePlayer1PokemonSprite(PokemonBaseSO pokemonBase)
    {
        foreach (Transform child in player1PokemonSprite.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        GameObject pokemonSprite = Instantiate(pokemonBase.BackSprite, player1PokemonSprite.transform);
        pokemonSprite.GetComponent<RectTransform>().anchoredPosition3D = Vector3.zero;
        
    }

    private void ChangePlayer2PokemonSprite(PokemonBaseSO pokemonBase)
    {
        foreach (Transform child in player2PokemonSprite.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        GameObject pokemonSprite = Instantiate(pokemonBase.FrontSprite, player2PokemonSprite.transform);
        pokemonSprite.GetComponent<RectTransform>().anchoredPosition3D = Vector3.zero;
    }

    public void AddTurnAction(ref Pokemon attacker, ref Pokemon target, MoveSO move)
    {
        turnMoveList.Add(new TurnMove(move, ref attacker, ref target));
    }
    public void ExecuteTurn()
    {
        //Debug.Log("***" + player1Pokemon);
        //Debug.Log("***" + player1.Party[0]);

        //On enter turn event (weather...)
        player1Pokemon.UpdateStats();
        player2Pokemon.UpdateStats();

        //Pokemon move event
        Debug.Log(turnMoveList.Count);
        for (int i = 0; i < turnMoveList.Count; i++)
        {
            if (turnMoveList[i].move.MovePower > 0)
            {
                Debug.Log(turnMoveList[i].move + " | " + turnMoveList[i].target);
                DoMoveDamage(ref turnMoveList[i].target, CalculateDamage(turnMoveList[i].attacker, turnMoveList[i].target, turnMoveList[i].move));
            }
        }
        //End turn event (burn, poison, leftover, ...)

        turnMoveList.Clear();

    }

    public void DoMoveDamage(ref Pokemon target, int damage)
    {
        Debug.Log(damage + " | " + target.Hp);
        target.Hp -= damage;
        
    }

    public void DoMoveEffect(ref Pokemon attacker, ref Pokemon target, MoveSO move)
    {
        move.MoveEffect.DoEffect(attacker, target, move);
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
            A = attacker.GetBuffLevel(Stats.Atk);
            D = target.GetBuffLevel(Stats.Def);
        }
        else if (move.MoveCategory == MoveCategory.Special)
        {
            A = attacker.GetBuffLevel(Stats.SpA);
            D = target.GetBuffLevel(Stats.SpD);
        }

        float targets = 1;

        float weather = 1;
        if (this.Weather == Weather.HarshSunlight)
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
        else if (this.Weather == Weather.Rain)
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
        switch (attacker.GetBuffLevel(Stats.Crit))
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

        float random = (Random.Range(85, 100)) / 100f;

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
        Debug.Log(level + " | " + targets + " | " + weather + " | " + critical + " | " + random + " | " + stab + " | " + typeEffectiveness + " | " + burn + " | " + other);
        return (int)damage;
    }

}

public class TurnMove
{
    public MoveSO move;
    public Pokemon attacker;
    public Pokemon target;

    public TurnMove(MoveSO move, ref Pokemon attacker, ref Pokemon target)
    {
        this.move = move;
        this.attacker = attacker;
        this.target = target;
    }


}

