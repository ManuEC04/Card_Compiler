using System.Collections;
using System.Collections.Generic;
using Compiler;
using Unity.VisualScripting;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] GameObject player1;
    [SerializeField] GameObject player2;
    [SerializeField] GameObject board;
    [SerializeField] Animator animator;
    public GameObject Board{get{return Board;}}
    public GameObject Player1 {get{return player1;}}
    public GameObject Player2 {get{return player2;}}
    public Dictionary<string , Player> Players {get;set;}
    Turn player1turn;
    Turn player2turn;
     bool created;
    static Game instance;
    public static Game Instance {get{return instance;}}

    void Start()
    {

        player1turn = player1.GetComponent<Player>().Turn;
        player2turn = player2.GetComponent<Player>().Turn;
        Players = new Dictionary<string, Player>();
        Players.Add("player1" , player1.GetComponent<Player>());
        Players.Add("player2" , player2.GetComponent<Player>());
    }
    void Update()
    {
     instance = this;
    }
    public void CheckTurn() 
    {
        animator.SetBool("StartTurn", player2turn.EndTurn);
        animator.SetBool("EndTurn", player1turn.EndTurn);
        if (player1turn.EndTurn && !player2turn.Passed)
        {
            player1turn.EndTurn = false;
            player2turn.StartMyTurn();
        }
        else if (player2turn.EndTurn && !player1turn.Passed)
        {
            player2turn.EndTurn = false;
            player1turn.StartMyTurn();
        }
    }
}
