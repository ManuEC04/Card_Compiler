using System;
using System.Collections.Generic;
namespace Compiler
{
    public class Concatenation : BinaryExpression
    {
        public override void Evaluate()
        {
            Left.Evaluate();
            Right.Evaluate();
            if((string)Value == "@@")
            {
                Value = (string)Left.Value + " " + (string)Right.Value;
            }
            else if ((string)Value == "@")
            {
                Value = (string)Left.Value + (string)Right.Value;
            }
        }
        public override bool CheckSemantic(Context Context , List<CompilingError> Errors , Scope scope)
        {
            if(Left.Type == ExpressionType.Text && Left.Type == ExpressionType.Text)
            {
                return true;
            }
            Errors.Add(new CompilingError (Position , ErrorCode.Invalid , "Invalid expression"));
            return false;
        }
        public Concatenation(Expression left , Expression right , object value , int position):base(value , left , right , ExpressionType.Concatenation , position)
        {}

    }
}