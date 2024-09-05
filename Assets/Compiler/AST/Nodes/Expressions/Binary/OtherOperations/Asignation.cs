using System.Collections.Generic;
using System.Linq.Expressions;

namespace Compiler
{
    public class Asignation : BinaryExpression
    {
        public override void Evaluate()
        {
            Left.Evaluate();
            Right.Evaluate();

            switch (Value)
            {
                case "=": Left.Value = Right.Value; break;

                case "-=":
                    double temp = (double)Left.Value;
                    temp -= (double)Right.Value;
                    Left.Value = temp;
                    break;

                case "+=":
                    double temporal = (double)Left.Value;
                    temporal -= (double)Right.Value;
                    Left.Value = temporal;
                    break;
            }
        }
        public override bool CheckSemantic(Context context, List<CompilingError> Errors, Scope scope)
        {
            return true;
        }

        public Asignation(Expression Left , Expression Right , object Value , int Position):base(Value ,Left , Right , ExpressionType.Asignation , Position)
        {

        }
    }
}