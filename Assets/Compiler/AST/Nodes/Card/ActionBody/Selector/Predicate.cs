
using System.Collections.Generic;
using System;
namespace Compiler
{
    public class Predicate : ASTNode
    {
       public bool Value {get; set;}
       public  Property Property {get; set;}
       public Comparation Comparation {get; set;}
        public Predicate(Property property , Comparation comparation)
        {
            Property = property;
            Comparation = comparation;
        }

        public override void Evaluate()
        {
            Property.Evaluate();
            Comparation.Left.Value = Property.Value;
            Func <Expression , Expression , bool> Equal =(a , b) => a == b;
            Value = Equal(Comparation.Left , Comparation.Right);
        }
        public override bool CheckSemantic(Context Context , List<CompilingError> Errors , Scope scope)
        {
            return true && Property.CheckSemantic(Context , Errors , scope) && Comparation.CheckSemantic(Context , Errors , scope);
        }
        public bool VerifyPredicate(Expression Left)
        {
            Comparation.Left = Left;
            Comparation.Evaluate();
            if((bool)Comparation.Value == true)
            {
                return true;
            }
            return false;
        }
        public void PrintPredicate()
        {
             Console.WriteLine("Este es el valor del predicado" + " " + Property.Value);
             Comparation.Evaluate();
             Console.WriteLine("Esta es la parte izquierda del predicado" + " " + Comparation.Left.Value);
             Console.WriteLine("Esta es la parte derecha del predicado" + " " + Comparation.Right.Value);

        }

    }
}