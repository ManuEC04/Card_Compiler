using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine.UIElements;

namespace Compiler
{
    public class Asignation : BinaryExpression
    {

        public override void Evaluate()
        {
                UnityEngine.Debug.Log("Evaluamos la parte izquierda de la asignacion");
                Left.Evaluate();
                UnityEngine.Debug.Log(Left.Value);
                UnityEngine.Debug.Log("Evaluamos la parte derecha de la asignacion");
                Right.Evaluate();


            switch (Value)
            {
                case "=": Left.Value = Right.Value; break;

                case "-=":
                   double temp = (double)Left.Value;
                   temp -= (double)Right.Value;
                   Left.Value = temp;


                   UnityEngine.Debug.Log("Power de la carta:" + " " + Left.Value);
                    break;

                case "+=":
                    double temporal = (double)Left.Value;
                   temporal += (double)Right.Value;
                   Left.Value = temporal;
                    break;
            }
  
            
        }
        public override bool CheckSemantic(Context context, List<CompilingError> Errors, Scope scope)
        {
            return true;
        }
        public override void ResetValues()
        {
            Left.ResetValues();
            Right.ResetValues();
        }

        public Asignation(Expression Left, Expression Right, object Value, int Position) : base(Value, Left, Right, ExpressionType.Asignation, Position)
        {

        }
    }
}