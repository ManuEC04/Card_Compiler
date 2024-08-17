namespace Compiler
{
    public class UnaryExpression : Expression
    {
        public override object Value {get; protected set;}
        Expression Right { get; set; }

        public UnaryExpression(object value, Expression right, ExpressionType type) : base(value, type)
        {
            Value = value;
            Right = right;
            type = ExpressionType.Unary;
        }
        public override void Evaluate()
        {
            Right.Evaluate();
            if ((string)Value == "-")
            {
                Value = -(double)Right.Value;
            }
        }

    }
}