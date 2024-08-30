namespace Compiler
{
    public class Mul : BinaryExpression
    {
        public override void Evaluate()
        {
            Left.Evaluate();
            Right.Evaluate();

            Value = (double)Left.Value * (double)Right.Value;
        }
        public Mul(Expression left, Expression right, object value , int position) : base(value,left, right , ExpressionType.Number , position) {

         }
    }
}