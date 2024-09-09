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
        UnityEngine.Debug.Log("Hay colision");
        if(player.Turn.DrawExecuted)
        { 
            UnityEngine.Debug.Log("Ya se draweo");
        CardOutput cardoutput = other.gameObject.GetComponent<CardOutput>();
        UnityCard card = cardoutput.Card;
        foreach (string range in card.Range)
        {
            if (range.Equals(Rowtag) && cardoutput.PlayerId == player.Id)
            {
                UnityEngine.Debug.Log("Coincide el tag");
                cards.Add(other.gameObject);
                player.Hand.GetCardList().Remove(other.gameObject);
                other.transform.SetParent(gameObject.transform, true);
                other.GetComponent<DragAndDrop>().IsOverDropZone = true;
                other.GetComponent<CardOutput>().OnHand = false;
                cards[cards.Count - 1].GetComponent<CardOutput>().ActivateOnActivation();
                if(cards[cards.Count - 1].GetComponent<CardOutput>().Card.Effect !=null)
                {
                 cards[cards.Count - 1].GetComponent<CardOutput>().ActivateEffect();
                }
                UpdateTotalPower();
                player.Turn.PlayMade = true;
                player.Turn.PassTurn();
                game.CheckTurn();
                return;
            }
        }
        }

    }
    public void UpdateTotalPower()
    {
        gamezone.UpdatePowerCounter();
    }
}
