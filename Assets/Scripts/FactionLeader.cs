using System.Collections;
using System.Collections.Generic;
using Compiler;
using UnityEngine;
using UnityEngine.EventSystems;

public class FactionLeader : MonoBehaviour , IPointerClickHandler
{
    [SerializeField] GameObject Leader;
    bool executed;
    public void FindLeader()
    {
        Deck deck = GetComponentInParent<GameZone>().GetComponentInParent<Player>().Deck;
        foreach(GameObject Card in deck.GetCardList())
        {
            if(Card.GetComponent<CardOutput>().Card.Range[0] == "FactionLeader")
            {
              Leader = Card;
              Card.transform.SetParent(this.transform , true);
              deck.GetCardList().Remove(Card);
              break;
            }
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if(!executed)
        {
            if(Leader.GetComponent<CardOutput>().Card.OnActivation !=null)
            {
                Leader.GetComponent<CardOutput>().Card.OnActivation.Evaluate();
                executed = true;
            }
            else if (Leader.GetComponent<CardOutput>().Card.Effect !=null)
            {
                Leader.GetComponent<CardOutput>().Card.Effect.DynamicInvoke();
                executed = true;
            }
        }
    }
}
