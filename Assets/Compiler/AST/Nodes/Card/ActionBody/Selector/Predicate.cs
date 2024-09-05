
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Diagnostics;
using System.Linq;
namespace Compiler
{
    public class Predicate : Expression
    {
       public override object Value {get;set;}
       public  Property Property {get; set;}
       public Comparation Comparation {get; set;}
        public Predicate(Property property , Comparation comparation , int Position) : base ("",ExpressionType.Predicate ,  Position)

        {   Property = property;
            Comparation = comparation;
            this.Position = Position;
        }

        public override void Evaluate()
        {
            Property.Evaluate();
            Comparation.Right.Evaluate();
            Comparation.Left.Value = Property.Value;
        }
        public override bool CheckSemantic(Context Context , List<CompilingError> Errors , Scope scope)
        {
            return true && Property.CheckSemantic(Context , Errors , scope) && Comparation.CheckSemantic(Context , Errors , scope);
        }
        public bool VerifyPredicate(UnityCard card)
        {
           switch(Property.Sintaxys)
           {
            case "Faction": 

            if(card.Faction == (string)Comparation.Right.Value)
            {
                UnityEngine.Debug.Log("Verifica el faction");
               return true;
            }
            return false;
    
            case "Type":   
            if(card.Type == (string)Comparation.Right.Value)
            {
               return true;
            }
            return false;

            case "Power":   

            if(card.Power == (double)Comparation.Right.Value)
            {
               return true;
            }
            return false;

            case "Range" : 

            if(card.Range.ToList() == Comparation.Right.Value)
            {
                return true;
            }
            return false;
 
            default: UnityEngine.Debug.Log("Carta no contiene la propiedad especificada"); break;
            
           }
           return false;
        }

    }
}