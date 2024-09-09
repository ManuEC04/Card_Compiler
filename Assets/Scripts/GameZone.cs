using System.Collections;
using System.Collections.Generic;
using Compiler;
using UnityEngine;

public class GameZone : MonoBehaviour , ICardContainer
{
   [SerializeField] Row melee;
   [SerializeField] Row ranged;
   [SerializeField] Row siege;
   [SerializeField] UnityEngine.UI.Text ShowTotalPower;
   public double TotalPower {get;set;}
   public Row Melee {get{return melee;}}
   public Row Ranged {get{return ranged;}}
   public Row Siege {get{return siege;}}
   public Row[] Rows {get;set;}
    List<GameObject> cards;

   void Start()
   {
      Rows = new Row[3];
      Rows[0] = Melee;
      Rows[1] = Ranged;
      Rows[2] = Siege; 
   }
   public void UpdatePowerCounter()
   {
      TotalPower = UpdateRowsPower();
      ShowTotalPower.text = TotalPower.ToString();
   }
   public double UpdateRowsPower()
   {
      double TotalPower = 0;
      foreach (Row row in Rows)
      {
         foreach(GameObject card in row.GetCardList())
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
      return cards;
   }

}
