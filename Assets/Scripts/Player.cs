using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] GameObject deck;
    [SerializeField] GameObject hand;
    [SerializeField] GameObject turn;
    [SerializeField] string id;
    public Deck Deck { get { return deck.GetComponent<Deck>(); } }
    public Hand Hand { get { return hand.GetComponent<Hand>(); } }
    public Turn Turn { get { return turn.GetComponent<Turn>(); } }
    public string Id { get { return id; } }

    public void Draw(int i) // Here we add i cards from the deck to the hand
    {
        if (!Turn.DrawExecuted)
        {
            for (int k = 0; k < i; k++)
            {
                Deck.PlayerDeck[0].transform.SetParent(Hand.transform, true);
                Hand.PlayerHand.Add(Deck.PlayerDeck[0]);
                Hand.PlayerHand[k].GetComponent<CardOutput>().OnHand = true;
                Deck.PlayerDeck.RemoveAt(0);
                Hand.CheckHandCount();
            }
            Turn.DrawExecuted = true;
        }
    }
    public void StartRoundDraw()
    {
        Draw(2);
    }
    void Start()
    {
        Draw(10);
    }

}
