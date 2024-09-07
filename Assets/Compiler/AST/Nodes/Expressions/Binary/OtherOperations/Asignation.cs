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

                    UnityEngine.Debug.Log(Left.Value);
                    break;

                case "+=":
                    Increase((double)Left.Value , (double)Right.Value);
                    break;
            }
            UnityEngine.Debug.Log("Este es el value despues de operar la asignacion" + Left.Value);
            
        }
        void Decrease(double Value , double Decrease)
        {
            Value -=Decrease;
        }
        void Increase(double Value , double Increase)
        {
            Value+=Increase;
        }
        public override bool CheckSemantic(Context context, List<CompilingError> Errors, Scope scope)
        {
            return true;
        }
        public override void ResetValues()
        {
            Left.ResetValues();
            Right.ResetValues();
            UnityEngine.Debug.Log("Se resetean los valores");
        }

        public Asignation(Expression Left, Expression Right, object Value, int Position) : base(Value, Left, Right, ExpressionType.Asignation, Position)
        {

        }
    }
}