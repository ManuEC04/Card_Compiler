using System.Collections;
using System.Collections.Generic;
using Compiler;
using UnityEngine;

public class Board : MonoBehaviour, ICardContainer {
    [SerializeField]GameZone player1zone;
    [SerializeField]GameZone player2zone;
    public GameZone Player1Zone {get {return player1zone;}}
    public GameZone Player2Zone {get{return player2zone;}}

    GameZone[] PlayersZones;

    void Start()
    {
        PlayersZones = new GameZone[2];
        PlayersZones[0] = player1zone;
        PlayersZones[1] = player2zone;
    }
      public void RemoveCard(GameObject value)
    {
      if(player1zone.GetCardList().Contains(value))
      {
       player1zone.RemoveCard(value);
      }
      else if (player2zone.GetCardList().Contains(value))
      {
       player2zone.RemoveCard(value);
      }  
    }
    public List<GameObject> GetCardList()
    {
      List<GameObject> cards = new List<GameObject>();
      foreach(GameObject card in player1zone.GetCardList())
      {
        cards.Add(card);
      }
      foreach(GameObject card in player2zone.GetCardList())
      {
        cards.Add(card);
      }
      return cards;
    }
    public void AddCard(GameObject value)
    {
      Context context = Context.Instance;
      string id = context.TriggerPlayer();

      if(id == "player1")
      {
          player1zone.AddCard(value);
      }
      else if (id == "player2")
      {
        player2zone.AddCard(value);
      }
    }


}
