using System.Collections;
using System.Collections.Generic;
using Compiler;
using Unity.VisualScripting;
using UnityEngine;

public class Row : MonoBehaviour , ICardContainer
{
    [SerializeField] string Rowtag;
    private List<GameObject> cards = new List<GameObject>();
    public List<GameObject> GetCardList()
    {
        return cards;
    }
    Game game;
    Player player;
    GameZone gamezone;
    void Start()
    {
        gamezone = GetComponentInParent<GameZone>();
        player = gamezone.GetComponentInParent<Player>();
        game = player.GetComponentInParent<Game>();
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        CardOutput cardoutput = other.gameObject.GetComponent<CardOutput>();
        UnityCard card = cardoutput.Card;
        foreach (string range in card.Range)
        {
            if (range.Equals(Rowtag) && cardoutput.PlayerId == player.Id)
            {
                cards.Add(other.gameObject);
                other.transform.SetParent(gameObject.transform, true);
                other.GetComponent<DragAndDrop>().IsOverDropZone = true;
                other.GetComponent<CardOutput>().OnHand = false;
                cards[cards.Count - 1].GetComponent<CardOutput>().ActivateEffect();
                player.Turn.PlayMade = true;
                player.Turn.PassTurn();
                game.CheckTurn();
            }
        }
    }
}
