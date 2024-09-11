using System.Collections;
using System.Collections.Generic;
using Compiler;
using Unity.VisualScripting;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] GameObject player1;
    Player player1component;
    Player player2component;
    [SerializeField] GameObject player2;
    [SerializeField] GameObject board;
    [SerializeField] Animator animator;
    public GameObject Board { get { return board; } }
    public GameObject Player1 { get { return player1; } }
    public GameObject Player2 { get { return player2; } }
    public Dictionary<string, Player> Players { get; set; }
    Turn player1turn;
    Turn player2turn;
    bool created;
    static Game instance;
    public static Game Instance { get { return instance; } }
    void Start()
    {

        player1component = player1.GetComponent<Player>();
        player2component = player2.GetComponent<Player>();
        player1turn = player1.GetComponent<Player>().Turn;
        player2turn = player2.GetComponent<Player>().Turn;
        player1turn.StartTurn = true;
        Players = new Dictionary<string, Player>();
        Players.Add("player1", player1.GetComponent<Player>());
        Players.Add("player2", player2.GetComponent<Player>());
    }
    void Update()
    {
        instance = this;
    }
    void CheckLifeCounter()
    {
        if (player1component.LifeCounter.Life == 0 && player2component.LifeCounter.Life > 0)
        {
            UnityEngine.Debug.Log("Gana el segundo jugador");
            Application.Quit();
        }
        else if (player2component.LifeCounter.Life == 0 && player1component.LifeCounter.Life > 0)
        {
            UnityEngine.Debug.Log("Gana el primer jugador");
            Application.Quit();
        }
        else if (player2component.LifeCounter.Life == 0 && player1component.LifeCounter.Life == 0)
        {
            UnityEngine.Debug.Log("Empate");
            Application.Quit();
        }
    }
    public void CheckTurn()
    {
        if (player1turn.Passed && player2turn.Passed)
        {
            EndRound();
        }
        else if (player1turn.EndTurn && !player2turn.Passed)
        {
            player1turn.EndTurn = false;
            player1.GetComponent<Player>().Hand.gameObject.SetActive(false);
            player2.GetComponent<Player>().Hand.gameObject.SetActive(true);
            player2turn.StartMyTurn();
        }
        else if (player1turn.EndTurn && player2turn.Passed)
        {
            player1turn.ContinueTurn();
        }
        else if (player2turn.EndTurn && !player1turn.Passed)
        {
            player2turn.EndTurn = false;
            player1.GetComponent<Player>().Hand.gameObject.SetActive(true);
            player2.GetComponent<Player>().Hand.gameObject.SetActive(false);
            player1turn.StartMyTurn();
        }
        else if (player2turn.EndTurn && player1turn.Passed)
        {
            player2turn.ContinueTurn();
        }
        animator.SetBool("Player2StartTurn", player2turn.StartTurn);
        animator.SetBool("Player1StartTurn", player1turn.StartTurn);
    }
    void EndRound()
    {

        if (player1component.Field.TotalPower > player2component.Field.TotalPower)
        {
            player2component.LifeCounter.Life--;
            player2component.LifeCounter.Destroylife();
            CheckLifeCounter();
            player1component.Turn.StartMyTurn();
        }
        else if (player1component.Field.TotalPower < player2component.Field.TotalPower)
        {
            player1component.LifeCounter.Life--;
            player1component.LifeCounter.Destroylife();
            CheckLifeCounter();
            player2component.Turn.StartMyTurn();
        }
        else
        {
            player2component.LifeCounter.Life--;
            player1component.LifeCounter.Life--;
            player1component.LifeCounter.Destroylife();
            player2component.LifeCounter.Destroylife();
            CheckLifeCounter();
            player1component.Turn.StartMyTurn();
        }
        player1component.ResetField();
        player2component.ResetField();
        player1component.Turn.Reset();
        player2component.Turn.Reset();
    }
}
