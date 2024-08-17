using System.Reflection.Metadata.Ecma335;

namespace Compiler
{
    public class BinaryExpression : Expression
    {
        public Expression Right {get; set;}
        public Expression Left {get;set;}
        public override object Value { get; protected set; }

        public BinaryExpression(object value , Expression left , Expression right , ExpressionType type):base(value , type)
        {
            Right = right;
            Left = left;
            Value = value;
        }

    }
}