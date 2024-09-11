using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graveyard : MonoBehaviour , ICardContainer
{
    public List<GameObject> cards = new List<GameObject>();
    public List<GameObject> Cards {get{return cards;}set{cards = value;}}
    public void SendToGraveyard(Row list)
    {
       foreach(GameObject item in list.GetCardList())
       {
         item.transform.SetParent(this.gameObject.transform , true);
         cards.Add(item);
       }
       list.GetCardList().Clear();
       list.UpdateTotalPower();
    }
    public void RemoveCard(GameObject value)
    {
      cards.Remove(value);
      value.transform.SetParent(gameObject.transform , false);
    }
    public List<GameObject> GetCardList()
    {
      return cards;
    }
  void Update()
  {
    foreach(GameObject card in cards)
        {
            card.transform.SetParent(gameObject.transform , true);
        }
    }
  }

