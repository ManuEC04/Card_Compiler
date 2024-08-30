
using System.Collections.Generic;

namespace Compiler
{
    public class BinaryExpression : Expression
    {
        public Expression Right {get; set;}
        public Expression Left {get;set;}
        public override object Value { get; set; }

        public override bool CheckSemantic(Context Context , List<CompilingError> Errors , Scope scope)
        {
            if(Left.Type == ExpressionType.Number || Right.Type ==ExpressionType.Number)
            {
                return true;
            }
             Errors.Add(new CompilingError(Position , ErrorCode.Invalid , "Invalid expression"));
            return false;
        }

        public BinaryExpression(object value , Expression left , Expression right , ExpressionType type , int position ):base(value , type , position)
        {
            Right = right;
            Left = left;
            Value = value;
        }

    }
}