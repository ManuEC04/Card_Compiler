using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Deck deck;
    [SerializeField] Hand hand;
    [SerializeField] Turn turn;
    [SerializeField] Graveyard graveyard;
    [SerializeField] string id;
    public Deck Deck { get { return deck; } }
    public Hand Hand { get { return hand; } }
    public Turn Turn { get { return turn; } }
    [SerializeField] Board board;
    public Board Board { get { return board; } }

    public Graveyard Graveyard { get { return graveyard; } }
    public string Id { get { return id; } }
    [SerializeField] Hand otherhand;
    public Hand Otherhand { get { return otherhand; } }
    [SerializeField] Deck otherdeck;
    public Deck Otherdeck { get { return otherdeck; } }
    [SerializeField] GameZone playerfield;
    public GameZone Field { get { return playerfield; } }
    [SerializeField] GameZone otherfield;
    public GameZone Otherfield { get { return otherfield; } }
    public LifeCounter LifeCounter {get;set;}

    public void Draw(int i) // Here we add i cards from the deck to the hand
    {

        if (!Turn.DrawExecuted)
        {

            for (int k = 0; k < i; k++)
            {
                    Deck.GetCardList()[0].transform.SetParent(Hand.transform, true);
                    Deck.GetCardList()[0].GetComponent<CardOutput>().OnHand = true;
                    Hand.GetCardList().Add(Deck.GetCardList()[0]);
                    Deck.GetCardList().RemoveAt(0);
                    Hand.CheckHandCount();
            }
            Turn.DrawExecuted = true;
        }
    }
    public void StartRoundDraw()
    {
        if (!turn.DrawExecuted)
        {
            Draw(2);
            turn.DrawExecuted = true;
        }
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
        LifeCounter = GetComponentInChildren<LifeCounter>();
        FactionLeader Leader = Field.GetComponentInChildren<FactionLeader>();
        Leader.FindLeader();
        Draw(10);
    }
    public void ResetField()
    {
        foreach (Row row in playerfield.Rows)
        {
            graveyard.SendToGraveyard(row);
        }
        Field.UpdatePowerCounter();
        playerfield.cards.Clear();
    }

}
