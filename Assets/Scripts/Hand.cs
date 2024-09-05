using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour , ICardContainer
{
   private List<GameObject> Cards = new List<GameObject>();
   public List<GameObject> GetCardList()
   {
    return Cards;
   }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CheckHandCount()
    {
        if(Cards.Count > 9)
        {
            //Here we send the cards to the graveyard
        }
    }
}
