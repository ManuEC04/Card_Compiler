using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Deck deck;
    [SerializeField] Hand hand;
    [SerializeField] Turn turn;
    public Dictionary<string , ICardContainer> PlayerComponents {get;set;}
    [SerializeField] string id;
    public Deck Deck { get { return deck; } }
    public Hand Hand { get { return hand; } }
    public Turn Turn { get { return turn; } }
    public string Id { get { return id; } }
    [SerializeField]Hand otherhand;
    public Hand Otherhand {get{return otherhand;}}
    [SerializeField]Deck otherdeck;
    public Deck Otherdeck {get{return otherdeck;}}
    [SerializeField]GameZone playerfield;
    public GameZone Field {get{return playerfield;}}
    [SerializeField]GameZone otherfield;
    public GameZone Otherfield {get{return otherfield;}}

    public void Draw(int i) // Here we add i cards from the deck to the hand
    {
        if (!Turn.DrawExecuted)
        {
            for (int k = 0; k < i; k++)
            {
                Deck.GetCardList()[0].transform.SetParent(Hand.transform, true);
                Hand.GetCardList().Add(Deck.GetCardList()[0]);
                Hand.GetCardList()[k].GetComponent<CardOutput>().OnHand = true;
                Deck.GetCardList().RemoveAt(0);
                Hand.CheckHandCount();
            }
            Turn.DrawExecuted = true;
        }
    }
    public void StartRoundDraw()
    {
        Draw(2);
    }
    public void Pass()
    {
        turn.Passed = true;
        turn.PassTurn();
        Game game = GetComponentInParent<Game>();
        game.CheckTurn();
    }
    void Start()
    {
        Draw(10);
    }

}
