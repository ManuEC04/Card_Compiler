using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

public class Hand : MonoBehaviour , ICardContainer
{
   [SerializeField]private List<GameObject> cards = new List<GameObject>();
   public List<GameObject> GetCardList()
   {
    return cards;
   }
    public void CheckHandCount()
    {
        if(cards.Count > 9)
        {
        //Here we send the cards to the graveyard
        }
    }
      public void RemoveCard(GameObject value)
    {
      cards.Remove(value);
      value.transform.SetParent(gameObject.transform , false);
    }
    public void AddCard(GameObject value)
    {
      cards.Add(value);
      value.transform.SetParent(gameObject.transform , true);
    }
    void Update()
    {
        foreach(GameObject card in cards)
        {
            card.GetComponent<CardOutput>().OnHand = true;
            card.transform.SetParent(gameObject.transform , true);
        }
    }
}
