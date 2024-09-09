using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactionLeader : MonoBehaviour
{
    [SerializeField] GameObject Leader;
    public void FindLeader()
    {
        Deck deck = GetComponentInParent<GameZone>().GetComponentInParent<Player>().Deck;
        foreach(GameObject Card in deck.GetCardList())
        {
            UnityEngine.Debug.Log("Estoy buscando el faction leader");
            if(Card.GetComponent<CardOutput>().Card.Range[0] == "FactionLeader")
            {
            UnityEngine.Debug.Log("Lo encontro");
              Leader = Card;
              Card.transform.SetParent(this.transform , true);
              deck.GetCardList().Remove(Card);
              break;
            }
        }
    }
}
