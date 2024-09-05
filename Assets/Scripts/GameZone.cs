using System.Collections;
using System.Collections.Generic;
using Compiler;
using UnityEngine;

public class GameZone : MonoBehaviour
{
   [SerializeField] Row melee;
   [SerializeField] Row ranged;
   [SerializeField] Row siege;
   public Row Melee {get{return melee;}}
   public Row Ranged {get{return ranged;}}
   public Row Siege {get{return siege;}}
   public Row[] Rows {get;set;}

   void Start()
   {
      Rows = new Row[3];
      Rows[0] = Melee;
      Rows[1] = Ranged;
      Rows[2] = Siege;
      
   }

}
