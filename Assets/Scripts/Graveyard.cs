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
    public List<GameObject> GetCardList()
    {
      return cards;
    }
}
