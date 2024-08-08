namespace Compiler
{
    public class BinaryExpression : Expression
    {
        public Expression Right {get; set;}
        public Expression Left {get;set;}

        public BinaryExpression(object value , ExpressionType type , Expression left , Expression right):base(value , type)
        {
            Right = right;
            Left = left;
        }

    }
}