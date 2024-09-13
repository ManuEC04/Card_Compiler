using System.Collections;
using System.Collections.Generic;
using Compiler;
using UnityEngine;

public class GameZone : MonoBehaviour , ICardContainer
{
   [SerializeField] Row melee;
   [SerializeField] Row ranged;
   [SerializeField] Row siege;
   [SerializeField] Row weather;
   [SerializeField] Row increase;
   [SerializeField] UnityEngine.UI.Text ShowTotalPower;
   public double TotalPower {get;set;}
   public Row Melee {get{return melee;}}
   public Row Ranged {get{return ranged;}}
   public Row Siege {get{return siege;}}
   public Row Weatherrow {get{return weather;}}
   public Row Increaserow {get{return increase;}}
   public Row[] Rows {get;set;}
    public List<GameObject> cards {get;set;}

   void Start()
   {
      cards = new List<GameObject>();
      Rows = new Row[5];
      Rows[0] = Melee;
      Rows[1] = Ranged;
      Rows[2] = Siege; 
      Rows[3] = Weatherrow;
      Rows[4] = Increaserow;
   }
   public void UpdatePowerCounter()
   {
      TotalPower = UpdateRowsPower();
      ShowTotalPower.text = TotalPower.ToString();
   }
   public double UpdateRowsPower()
   {
      double TotalPower = 0;
      for (int i = 0 ; i < 3 ; i++)
      {
         foreach(GameObject card in Rows[i].GetCardList())
         {
              TotalPower+=card.GetComponent<CardOutput>().PowerValue;
         }
         if(TotalPower < 0)
         {
            TotalPower = 0;
         }
      }
      return TotalPower;
   }
   public List<GameObject> GetCardList()
   {
      List<GameObject> cards = new List<GameObject>();
      foreach(Row row in Rows)
      {
         foreach(GameObject card in row.GetCardList())
         {
            cards.Add(card);
         }
      }
      return cards;
   }
     public void RemoveCard(GameObject value)
    {
      melee.RemoveCard(value);
      ranged.RemoveCard(value);
      siege.RemoveCard(value);
      weather.RemoveCard(value);
      increase.RemoveCard(value);
    }
    public void AddCard(GameObject value)
    {
      melee.AddCard(value);
    }

}
