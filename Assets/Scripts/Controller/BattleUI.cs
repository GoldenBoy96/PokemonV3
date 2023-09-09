using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum BattleMenuState { Home, Fight, Bag, Pokemon, Dialog }
public class BattleUI : MonoBehaviour
{
    public static BattleUI Instance { get; private set; }

    [SerializeField] private float letterPerSecond;

    [SerializeField] private GameObject battleSystem;

    [SerializeField] private TextMeshProUGUI dialogText;

    [SerializeField][TextArea] private string runText = "";

    //Home menu
    [SerializeField] private GameObject menuHome;
    [SerializeField] private Button btnFight;
    [SerializeField] private Button btnPokemon;
    [SerializeField] private Button btnBag;
    [SerializeField] private Button btnRun;

    //Fight menu
    [SerializeField] private GameObject menuFight;
    [SerializeField] private List<Button> btnMove;

    bool isTypingText = false;

    //private void OnEnable()
    //{
    //    isTypingText = false;

    //    switch (GameController.Instance.CurrentGameState)
    //    {
    //        case GameState.BattleWildPokemon:
    //            StartCoroutine(TypeStartWildBattleText("A WILD " + BattleController.Instance.Player2Pokemon.NameString + "APPEARED!"));
    //            break;
    //        default:
    //            break;
    //    }
    //}

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


    }
    private void OnEnable()
    {

    }
    private void Start()
    {
        isTypingText = false;

    }
    private void Update()
    {
        //this should be change to event
        if (isTypingText)
        {
            btnFight.interactable = false;
            btnPokemon.interactable = false;
            btnBag.interactable = false;
            btnRun.interactable = false;
        }
        else
        {
            btnFight.interactable = true;
            btnPokemon.interactable = true;
            btnBag.interactable = true;
            btnRun.interactable = true;
        }
    }

    public void Fight()
    {
        menuFight.SetActive(true);
        foreach (Button btn in btnMove)
        {
            btn.enabled = true;
        }

        for (int i = 0; i < 4; i++)
        {
            if (BattleController.Instance.player1Pokemon.Moveset.Count > i)
            {
                btnMove[i].GetComponentInChildren<TextMeshProUGUI>().SetText(BattleController.Instance.player1Pokemon.Moveset[i].Move.MoveName);
            }
            else
            {
                btnMove[i].GetComponentInChildren<TextMeshProUGUI>().text = "";
                btnMove[i].enabled = false;
            }
        }
    }


    public void Run()
    {
        if (!isTypingText)
        {
            StartCoroutine(TypeRunText(runText));
        }

    }

    public void StartBattle()
    {
        isTypingText = false;

        switch (GameController.Instance.CurrentGameState)
        {
            case GameState.BattleWildPokemon:
                StartCoroutine(TypeStartWildBattleText());
                break;
            default:
                break;
        }

    }

    public IEnumerator TypeStartWildBattleText()
    {
        menuHome.SetActive(false);
        yield return StartCoroutine(TypeDialog("A wild " + BattleController.Instance.player2Pokemon.PokemonBaseSO.PokemonName + " appeared!"));
        yield return StartCoroutine(TypeDialog("Go! " + BattleController.Instance.player1Pokemon.NameString + "!"));
        menuHome.SetActive(true);
    }

    public IEnumerator TypeRunText(string text)
    {
        yield return StartCoroutine(TypeDialog(text));
        GameController.Instance.SwitchStateToOutWorld();

    }

    public IEnumerator TypeDialog(string text)
    {
        isTypingText = true;
        dialogText.text = "";
        foreach (var letter in text)
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(1f / letterPerSecond);
        }
        isTypingText = false;
    }
}
