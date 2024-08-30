using System;
using System.Collections.Generic;
namespace Compiler
{
    public class Logic : BinaryExpression
    {
        public override void Evaluate()
        {
            Left.Evaluate();
            Right.Evaluate();
            if ((string)Value == "&&")
            {
                Value = (bool)Left.Value && (bool)Right.Value;
            }
            else if ((string)Value == "||")
            {
                Value = (bool)Left.Value || (bool)Right.Value;
            }
        }
        public override bool CheckSemantic(Context Context , List<CompilingError> Errors , Scope scope)
        {
            if (Left.Type == ExpressionType.Boolean || Right.Type == ExpressionType.Boolean)
            {
                return true;
            }
             if (Left.Type == ExpressionType.Number || Right.Type == ExpressionType.Number)
            {
                return true;
            }
             Errors.Add(new CompilingError(Position , ErrorCode.Invalid , "Invalid expression"));
            return false;
        }
        public Logic(Expression left, Expression right, object value , int position) : base(value, left, right, ExpressionType.Boolean , position)
        { }
    }
}