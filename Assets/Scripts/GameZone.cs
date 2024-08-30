using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameZone : MonoBehaviour
{
   [SerializeField] Row melee;
   [SerializeField] Row ranged;
   [SerializeField] Row siege;
   public Row Melee {get{return melee;}}
   public Row Ranged {get{return ranged;}}
   public Row Siege {get{return siege;}}

}
