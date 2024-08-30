
using System;
using System.Collections.Generic;
namespace Compiler
{
    public class Elevate : BinaryExpression
    {
        public override void Evaluate()
        {
            Left.Evaluate();
            Right.Evaluate();
            Value = Math.Pow((double)Left.Value , (double)Right.Value);
        }
        public Elevate(Expression left , Expression right , object value , int position):base(value , left , right , ExpressionType.Elevate , position)
        {
        } 
    }
}