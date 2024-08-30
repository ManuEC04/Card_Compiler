namespace Compiler
{
    public class Div : BinaryExpression
    {
        public override void Evaluate()
        {
            Left.Evaluate();
            Right.Evaluate();

            Value = (double)Left.Value / (double)Right.Value;
        }
        public Div(Expression left , Expression right , object value , int position ):base(value , left , right , ExpressionType.Div , position)
        {
        }
    }
}