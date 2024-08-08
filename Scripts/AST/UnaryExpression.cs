namespace Compiler
{
    public class UnaryExpression : Expression
    {
        Expression Right { get; set; }

        public UnaryExpression(object value, Expression right, ExpressionType type) : base(value, type)
        {
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